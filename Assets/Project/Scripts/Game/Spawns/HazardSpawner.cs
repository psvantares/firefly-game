// Copyright (C) 2022 Geronimo Games - All Rights Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.

using System;
using System.Collections;
using GeronimoGames.Firefly.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GeronimoGames.Firefly.Game
{
    public class HazardSpawner : MonoBehaviour
    {
        [Header("PREFABS")] [SerializeField] private HazardBase hazardBasePrefab;

        private const int RandomSpawn = 10;

        private EntitiesPool<HazardBase> hazardPool;

        private Vector2 min;
        private Vector2 max;

        public event Action OnKill;
        public event Action<int> OnAddPoints;

        private void Awake()
        {
            hazardPool = new EntitiesPool<HazardBase>(hazardBasePrefab, transform);

            var mainCamera = Camera.main;

            if (mainCamera == null)
            {
                return;
            }

            min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        }

        public void StartHazardSpawner()
        {
            StartCoroutine(SpawnRandomHazard());
        }

        public void StopHazardSpawner()
        {
            StopAllCoroutines();
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator SpawnRandomHazard()
        {
            var random = Random.Range(1, RandomSpawn);

            yield return new WaitForSeconds(random);

            var hazard = hazardPool.Rent();
            hazard.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
            hazard.OnClear += HandleHazardClear;
            hazard.OnAddPoints += HandleAddPoints;
            hazard.OnKill += HandleKill;

            yield return StartCoroutine(SpawnRandomHazard());
        }

        private void HandleHazardClear(HazardBase hazardBase)
        {
            hazardBase.OnClear -= HandleHazardClear;
            hazardBase.OnAddPoints -= HandleAddPoints;
            hazardBase.OnKill -= HandleKill;
            hazardPool.Return(hazardBase);
        }

        private void HandleAddPoints(int points)
        {
            OnAddPoints?.Invoke(points);
        }

        private void HandleKill()
        {
            OnKill?.Invoke();
        }
    }
}