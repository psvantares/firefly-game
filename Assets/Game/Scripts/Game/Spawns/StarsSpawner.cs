using Game.Components;
using UnityEngine;

namespace Game
{
    public class StarsSpawner : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField]
        private bool enableBackground = true;

        [SerializeField]
        private bool onlyWhiteStars;

        [SerializeField]
        private int maxStars1 = 35;

        [SerializeField]
        private int maxStars2 = 45;

        [Header("GAME OBJECTS")]
        [SerializeField]
        private GameObject[] stars1;

        [SerializeField]
        private GameObject[] stars2;

        private Camera mainCamera;
        private Vector2 min;
        private Vector2 max;

        private readonly Color[] starColors =
        {
            new(0.5f, 0.5f, 1f),
            new(0, 1f, 1f),
            new(1f, 1f, 1f)
        };

        private void Awake()
        {
            mainCamera = Camera.main;

            if (mainCamera == null)
            {
                return;
            }

            min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        }

        public void Initialize()
        {
            for (var i = 0; i < maxStars1; ++i)
            {
                var star = Instantiate(stars1[Random.Range(0, stars1.Length)], transform, true);

                star.GetComponent<SpriteRenderer>().color = onlyWhiteStars ? Color.white : starColors[i % starColors.Length];
                star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
                star.GetComponent<Star>().MoveSpeed = -(4.5f * Random.value + 0.5f);
            }

            if (enableBackground)
            {
                BackgroundStarField();
            }
        }

        private void BackgroundStarField()
        {
            for (var i = 0; i < maxStars2; ++i)
            {
                var bgStar = Instantiate(stars2[Random.Range(0, stars2.Length)], transform, true);

                bgStar.GetComponent<SpriteRenderer>().color = Color.white;
                bgStar.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
                bgStar.GetComponent<Star>().MoveSpeed = -(0.1f * Random.value + 0.2f);
            }
        }
    }
}