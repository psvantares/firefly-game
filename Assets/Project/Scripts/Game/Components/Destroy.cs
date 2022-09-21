// Copyright (C) 2022 Geronimo Games - All Rights Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.

using UnityEngine;

namespace GeronimoGames.Firefly.Game
{
    public class Destroy : MonoBehaviour
    {
        [Header("SETTINGS")] [SerializeField] private float destroyTime = 0.5f;

        private void Start()
        {
            if (destroyTime != 0)
            {
                Invoke(nameof(DestroyGameObject), destroyTime);
            }
        }

        private void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}