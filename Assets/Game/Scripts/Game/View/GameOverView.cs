using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    public class GameOverView : Base.View
    {
        [Header("BUTTONS")]
        [SerializeField]
        private Button menuButton;

        [SerializeField]
        private Button resetButton;

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