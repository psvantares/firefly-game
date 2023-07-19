using System;
using System.Collections;
using Game.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("PREFABS")]
        [SerializeField]
        private EnemyBase enemyBomber;

        [SerializeField]
        private EnemyBase enemyFighter;

        [SerializeField]
        private EnemyBase enemyOrbWeapon;

        private Vector2 min;
        private Vector2 max;

        private EntitiesPool<EnemyBase> bomberPool;
        private EntitiesPool<EnemyBase> fighterPool;
        private EntitiesPool<EnemyBase> orbWeaponPool;

        public event Action OnKill;
        public event Action<int> OnAddPoints;

        private float bomberSecond;
        private float fighterSecond;
        private float orbWeaponSecond;

        private void Awake()
        {
            var tr = transform;
            bomberPool = new EntitiesPool<EnemyBase>(enemyBomber, tr);
            fighterPool = new EntitiesPool<EnemyBase>(enemyFighter, tr);
            orbWeaponPool = new EntitiesPool<EnemyBase>(enemyOrbWeapon, tr);

            var mainCamera = Camera.main;

            if (mainCamera == null)
            {
                return;
            }

            min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        }

        public void StartEnemySpawner()
        {
            bomberSecond = 4f;
            fighterSecond = 7f;
            orbWeaponSecond = 9f;

            StartCoroutine(SpawnBomber());
            StartCoroutine(SpawnFighter());
            StartCoroutine(SpawnOrbWeapon());
        }

        public void StopEnemySpawner()
        {
            StopAllCoroutines();
        }

        public void SetTimeSpawn()
        {
            if (bomberSecond <= 1)
            {
                bomberSecond = 0.8f;
            }
            else
            {
                bomberSecond -= 0.3f;
            }

            if (fighterSecond <= 1)
            {
                fighterSecond = 0.9f;
            }
            else
            {
                fighterSecond -= 0.5f;
            }

            if (orbWeaponSecond <= 1)
            {
                orbWeaponSecond = 1f;
            }
            else
            {
                orbWeaponSecond -= 0.5f;
            }
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator SpawnBomber()
        {
            yield return new WaitForSeconds(bomberSecond);

            var enemy = bomberPool.Rent();
            enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
            enemy.OnClear += HandleBomberClear;
            enemy.OnAddPoints += HandleAddPoints;
            enemy.OnKill += HandleKill;

            yield return StartCoroutine(SpawnBomber());
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator SpawnFighter()
        {
            yield return new WaitForSeconds(fighterSecond);

            var enemy = fighterPool.Rent();
            enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
            enemy.OnClear += HandleFighterClear;
            enemy.OnAddPoints += HandleAddPoints;
            enemy.OnKill += HandleKill;

            yield return StartCoroutine(SpawnFighter());
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator SpawnOrbWeapon()
        {
            yield return new WaitForSeconds(orbWeaponSecond);

            var enemy = orbWeaponPool.Rent();
            enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
            enemy.OnClear += HandleOrbWeaponClear;
            enemy.OnAddPoints += HandleAddPoints;
            enemy.OnKill += HandleKill;

            yield return StartCoroutine(SpawnOrbWeapon());
        }

        private void HandleBomberClear(EnemyBase enemyBase)
        {
            enemyBase.OnClear -= HandleBomberClear;
            enemyBase.OnAddPoints -= HandleAddPoints;
            enemyBase.OnKill -= HandleKill;
            bomberPool.Return(enemyBase);
        }

        private void HandleFighterClear(EnemyBase enemyBase)
        {
            enemyBase.OnClear -= HandleFighterClear;
            enemyBase.OnAddPoints -= HandleAddPoints;
            enemyBase.OnKill -= HandleKill;
            fighterPool.Return(enemyBase);
        }

        private void HandleOrbWeaponClear(EnemyBase enemyBase)
        {
            enemyBase.OnClear -= HandleOrbWeaponClear;
            enemyBase.OnAddPoints -= HandleAddPoints;
            enemyBase.OnKill -= HandleKill;
            orbWeaponPool.Return(enemyBase);
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