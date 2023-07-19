using System.Collections;
using UnityEngine;

namespace Game
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [Header("GAME OBJECTS")]
        [SerializeField]
        private GameObject bluePowerUp;

        [SerializeField]
        private GameObject greenPowerUp;

        [SerializeField]
        private GameObject redPowerUp;

        [SerializeField]
        private GameObject brownPowerUp;

        private Vector2 min;
        private Vector2 max;

        private void Awake()
        {
            var mainCamera = Camera.main;

            if (mainCamera == null)
            {
                return;
            }

            min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        }

        public void StartPowerUpSpawner()
        {
            StartCoroutine(SpawnRandomPowerUp(5f));
        }

        public void StopPowerUpSpawner()
        {
            StopAllCoroutines();
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator SpawnRandomPowerUp(float spawnTime)
        {
            var random = Random.Range(0, 4);
            yield return new WaitForSeconds(spawnTime);
            switch (random)
            {
                case 0:
                {
                    var enemy = Instantiate(bluePowerUp);
                    enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
                    break;
                }
                case 1:
                {
                    var enemy = Instantiate(greenPowerUp);
                    enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
                    break;
                }
                case 2:
                {
                    var enemy = Instantiate(redPowerUp);
                    enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
                    break;
                }
                case 3:
                {
                    var enemy = Instantiate(brownPowerUp);
                    enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
                    break;
                }
            }

            yield return StartCoroutine(SpawnRandomPowerUp(10f));
        }
    }
}