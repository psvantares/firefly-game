using System;
using TMPro;
using UnityEngine;

namespace Game
{
    public class HudView : View
    {
        [Header("TEXTS")]
        [SerializeField]
        private TMP_Text healthText;

        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField]
        private TMP_Text timerText;

        [SerializeField]
        private TMP_Text levelText;

        [SerializeField]
        private TMP_Text destroyedText;

        private int level;

        public event Action OnLevelUp;

        public int CurrentScore { get; private set; }
        public int CurrentDestroyedCount { get; private set; }

        public void Initialize()
        {
            level = 0;

            SetHealth(100);
            SetScore(0);
            SetTimer("00:00");
            SetLevel(0);
            SetDestroyed(0);
        }

        public void UpdateHealth(int health)
        {
            SetHealth(health);
        }

        public void AddScore(int score)
        {
            CurrentScore += score;
            SetScore(CurrentScore);
            CheckLevel(CurrentScore);
        }

        public void Kill()
        {
            CurrentDestroyedCount++;
            SetDestroyed(CurrentDestroyedCount);
        }

        public void SetTimer(string timer)
        {
            timerText.text = $"{timer}";
        }

        private void SetLevel(int level)
        {
            levelText.text = $"LEVEL: {level}";
        }

        private void SetDestroyed(int destroyed)
        {
            destroyedText.text = $"DESTROYED: {destroyed}";
        }

        private void SetHealth(int health)
        {
            healthText.text = $"HEALTH: {health}";
        }

        private void SetScore(int score)
        {
            scoreText.text = $"SCORE: {score}";
            CurrentScore = score;
        }

        private void CheckLevel(int points)
        {
            var isNewLevel = points / (1000 * (level + 1)) > level;

            if (!isNewLevel)
            {
                return;
            }

            level++;

            SetLevel(level);

            OnLevelUp?.Invoke();
        }
    }
}