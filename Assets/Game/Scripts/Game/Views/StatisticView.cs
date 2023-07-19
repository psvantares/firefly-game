using System;
using Game.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class StatisticView : View
    {
        [Header("BUTTONS")]
        [SerializeField]
        private Button closeButton;

        [Header("TEXTS")]
        [SerializeField]
        private TMP_Text gameTimeText;

        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField]
        private TMP_Text destroyedText;

        public event Action<bool> OnClose;

        private void OnEnable()
        {
            closeButton.onClick.AddListener(() => { OnClose?.Invoke(false); });
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveAllListeners();
        }

        public void Initialize()
        {
            gameTimeText.text = Storage.GetString("game_time", "00:00:00");
            scoreText.text = Storage.GetString("game_score", "0");
            destroyedText.text = Storage.GetString("game_destroyed", "0");
        }
    }
}