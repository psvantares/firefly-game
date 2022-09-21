// Copyright (C) 2022 Geronimo Games - All Rights Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.

using System;
using UnityEngine;
using UnityEngine.UI;

namespace GeronimoGames.Firefly.Game
{
    public class MenuView : View
    {
        [Header("VIEWS")] [SerializeField] private SettingsView settingsView;
        [SerializeField] private StatisticView statisticView;
        [Header("BUTTONS")] [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button statisticsButton;

        public event Action OnPlay;

        protected override void Start()
        {
            base.Start();

            settingsView.Initialize();
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(() => OnPlay?.Invoke());
            settingsButton.onClick.AddListener(() => HandleSettings(true));
            statisticsButton.onClick.AddListener(() => HandleStatistic(true));

            settingsView.OnClose += HandleSettings;
            statisticView.OnClose += HandleStatistic;
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            statisticsButton.onClick.RemoveAllListeners();

            settingsView.OnClose -= HandleSettings;
            statisticView.OnClose -= HandleStatistic;
        }

        private void HandleSettings(bool active)
        {
            SetActive(!active);
            settingsView.SetActive(active);
        }

        private void HandleStatistic(bool active)
        {
            SetActive(!active);

            if (active)
            {
                statisticView.Initialize();
            }

            statisticView.SetActive(active);
        }
    }
}