using System;
using Game.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SettingsView : View
    {
        [Header("BUTTONS")]
        [SerializeField]
        private Button closeButton;

        [Header("TOGGLES")]
        [SerializeField]
        private Toggle musicToggle;

        public event Action<bool> OnClose;

        private bool isMusic;
        private bool isParticles;

        private void OnEnable()
        {
            closeButton.onClick.AddListener(() => { OnClose?.Invoke(false); });

            musicToggle.onValueChanged.AddListener(value =>
            {
                AudioListener.pause = !value;
                isMusic = AudioListener.pause;

                Storage.SetBool("game_music", isMusic);
            });
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveAllListeners();
            musicToggle.onValueChanged.RemoveAllListeners();
        }

        public void Initialize()
        {
            musicToggle.isOn = !Storage.GetBool("game_music");
            AudioListener.pause = Storage.GetBool("game_music");
        }
    }
}