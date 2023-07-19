using System;
using Game.Components;
using Game.Models;
using Game.Utilities;
using UnityEngine;

namespace Game.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("VIEWS")]
        [SerializeField]
        private PreloaderView preloaderView;

        [SerializeField]
        private MenuView menuView;

        [SerializeField]
        private HudView hudView;

        [SerializeField]
        private GameOverView gameOverView;

        [Header("COMPONENTS")]
        [SerializeField]
        private PlayerControl playerControl;

        [SerializeField]
        private TimerGame timerGame;

        [Header("SPAWNS")]
        [SerializeField]
        private StarsSpawner starsSpawner;

        [SerializeField]
        private EnemySpawner enemySpawner;

        [SerializeField]
        private HazardSpawner hazardSpawner;

        [SerializeField]
        private PowerUpSpawner powerUpSpawner;

        private void Awake()
        {
            Application.targetFrameRate = 90;

            preloaderView.Render(() => SetGameState(GameState.Opening));
        }

        private void Start()
        {
            starsSpawner.Initialize();
        }

        private void OnEnable()
        {
            hudView.OnLevelUp += HandleLevel;
            menuView.OnPlay += HandlePlay;
            timerGame.OnTimer += HandleTimer;
            gameOverView.OnMenu += HandleMenu;
            gameOverView.OnReset += HandleReset;

            playerControl.OnPlayerHealth += HandlePlayerHealth;
            playerControl.OnGameOver += HandlePlayerGameOver;

            enemySpawner.OnAddPoints += HandleAddPoints;
            enemySpawner.OnKill += HandleKill;
            hazardSpawner.OnAddPoints += HandleAddPoints;
            hazardSpawner.OnKill += HandleKill;
        }

        private void OnDisable()
        {
            hudView.OnLevelUp -= HandleLevel;
            menuView.OnPlay -= HandlePlay;
            timerGame.OnTimer -= HandleTimer;
            gameOverView.OnMenu -= HandleMenu;
            gameOverView.OnReset -= HandleReset;

            playerControl.OnPlayerHealth -= HandlePlayerHealth;
            playerControl.OnGameOver -= HandlePlayerGameOver;

            enemySpawner.OnAddPoints -= HandleAddPoints;
            enemySpawner.OnKill -= HandleKill;
            hazardSpawner.OnAddPoints -= HandleAddPoints;
            hazardSpawner.OnKill -= HandleKill;
        }

        private void SetGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Opening:
                    Opening();
                    break;
                case GameState.Play:
                    Play();
                    break;
                case GameState.GameOver:
                    GameOver();
                    break;
                case GameState.Reset:
                    Restart();
                    break;
            }
        }

        private void Opening()
        {
            hudView.SetActive(false);
            preloaderView.SetActive(false);
            gameOverView.SetActive(false);
            menuView.SetActive(true);
        }

        private void Play()
        {
            hudView.SetActive(true);
            menuView.SetActive(false);
            preloaderView.SetActive(false);
            gameOverView.SetActive(false);

            timerGame.Run();
            hudView.Initialize();

            playerControl.Initialize();

            enemySpawner.StartEnemySpawner();
            hazardSpawner.StartHazardSpawner();
            powerUpSpawner.StartPowerUpSpawner();
        }

        private void GameOver()
        {
            SaveStatistic();

            hudView.SetActive(false);
            menuView.SetActive(false);
            preloaderView.SetActive(false);
            gameOverView.SetActive(true);

            playerControl.ResetPowerUp();
            timerGame.Stop();

            enemySpawner.StopEnemySpawner();
            hazardSpawner.StopHazardSpawner();
            powerUpSpawner.StopPowerUpSpawner();
        }

        private void Restart()
        {
            Play();
        }

        private void SaveStatistic()
        {
            var newTime = TimeSpan.FromMinutes(timerGame.Minutes) + TimeSpan.FromSeconds(timerGame.Seconds);
            var totalTime = newTime;
            var gameTime = $"{totalTime.Hours:00}:{totalTime.Minutes:00}:{totalTime.Seconds:00}";
            var score = hudView.CurrentScore.ToString();
            var destroyed = hudView.CurrentDestroyedCount.ToString();

            Storage.SetString("game_time", gameTime);
            Storage.SetString("game_score", score);
            Storage.SetString("game_destroyed", destroyed);
        }

        private void HandlePlay()
        {
            Play();
        }

        private void HandlePlayerHealth(int health)
        {
            hudView.UpdateHealth(health);
        }

        private void HandlePlayerGameOver()
        {
            GameOver();
        }

        private void HandleTimer(string timer)
        {
            hudView.SetTimer(timer);
        }

        private void HandleMenu()
        {
            SetGameState(GameState.Opening);
        }

        private void HandleReset()
        {
            SetGameState(GameState.Reset);
        }

        private void HandleAddPoints(int points)
        {
            hudView.AddScore(points);
        }

        private void HandleKill()
        {
            hudView.Kill();
        }

        private void HandleLevel()
        {
            enemySpawner.SetTimeSpawn();
        }
    }
}