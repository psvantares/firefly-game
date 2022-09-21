// Copyright (C) 2022 Geronimo Games - All Rights Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.

using System;
using UnityEngine;
using UnityEngine.UI;

namespace GeronimoGames.Firefly.Game
{
    public class GameOverView : View
    {
        [Header("BUTTONS")] [SerializeField] private Button menuButton;
        [SerializeField] private Button resetButton;

        public event Action OnMenu;
        public event Action OnReset;

        private void OnEnable()
        {
            menuButton.onClick.AddListener(() => OnMenu?.Invoke());
            resetButton.onClick.AddListener(() => OnReset?.Invoke());
        }

        private void OnDisable()
        {
            menuButton.onClick.RemoveAllListeners();
            resetButton.onClick.RemoveAllListeners();
        }
    }
}