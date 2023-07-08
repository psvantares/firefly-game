using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    public class SettingsView : Base.View
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

                Prefs.Prefs.SetBool("game_music", isMusic);
            });
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveAllListeners();
            musicToggle.onValueChanged.RemoveAllListeners();
        }

        public void Initialize()
        {
            musicToggle.isOn = !Prefs.Prefs.GetBool("game_music");
            AudioListener.pause = Prefs.Prefs.GetBool("game_music");
        }
    }
}