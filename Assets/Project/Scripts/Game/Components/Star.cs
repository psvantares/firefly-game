// Copyright (C) 2022 Geronimo Games - All Rights Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.

using UnityEngine;
using Random = UnityEngine.Random;

namespace GeronimoGames.Firefly.Game
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1.0f;

        private Vector2 min;
        private Vector2 max;

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        private void Awake()
        {
            var mainCamera = Camera.main;

            if (mainCamera == null)
            {
                return;
            }

            min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        }

        private void Update()
        {
            var tr = transform;
            Vector2 position = tr.position;
            position = new Vector2(position.x, position.y + MoveSpeed * Time.deltaTime);
            tr.position = position;

            if (transform.position.y < min.y)
            {
                transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
            }
        }
    }
}