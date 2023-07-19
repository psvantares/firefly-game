using System;
using System.Collections;
using Game.Components;
using UnityEngine;

namespace Game
{
    public class EnemyBase : MonoBehaviour
    {
        [Header("GAME OBJECTS")]
        [SerializeField]
        private GameObject explosion;

        [Header("COMPONENTS")]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private FloatingCombat floatingCombat;

        [Header("SETTINGS")]
        [SerializeField]
        private int currentHealth;

        [SerializeField]
        private int pointsValue;

        private Camera mainCamera;

        public event Action<EnemyBase> OnClear;
        public event Action OnKill;
        public event Action<int> OnAddPoints;

        private Vector2 min;
        private const int MAX_HEALTH = 100;

        public int PointsValue
        {
            get => pointsValue;
            set => pointsValue = value;
        }

        private void Awake()
        {
            mainCamera = Camera.main;

            if (mainCamera != null)
            {
                min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            }
        }

        private void OnEnable()
        {
            currentHealth = MAX_HEALTH;
        }

        private void Update()
        {
            if (!(transform.position.y < min.y))
            {
                return;
            }

            OnClear?.Invoke(this);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(GameTag.PlayerShip) || col.CompareTag(GameTag.PlayerBullet))
            {
                StartCoroutine(FlashSprite());
                currentHealth -= 35;

                switch (currentHealth)
                {
                    case > 0:
                        return;
                    case <= 0:
                        OnKill?.Invoke();
                        OnAddPoints?.Invoke(PointsValue);

                        floatingCombat.ShowCombatText();

                        PlayExplosion();
                        Destroy(gameObject);
                        break;
                }
            }

            if (!col.CompareTag(GameTag.Bomb))
            {
                return;
            }

            StartCoroutine(FlashSprite());
            PlayExplosion();

            OnAddPoints?.Invoke(PointsValue);

            Destroy(gameObject);
        }

        private IEnumerator FlashSprite()
        {
            var material = spriteRenderer.material;
            material.color = new Color(255f, 225f, 255f, 255f);
            yield return new WaitForSeconds(0.1f);
            material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        private void PlayExplosion()
        {
            var instantiate = Instantiate(explosion);
            instantiate.transform.position = transform.position;
        }
    }
}