// Copyright (C) 2022 Geronimo Games - All Rights Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.

using UnityEngine;

namespace GeronimoGames.Firefly.Game
{
    public class View : SafeArea
    {
        [Header("ROOT")] [SerializeField] private GameObject root;

        public void SetActive(bool value)
        {
            if (root.activeSelf == value)
            {
                return;
            }

            root.SetActive(value);
        }
    }
}