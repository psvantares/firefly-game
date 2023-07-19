using System;
using System.Collections;
using Game.Components;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class HazardBase : MonoBehaviour
    {
        [Header("GAME OBJECTS")]
        [SerializeField]
        private GameObject hazardExplosion;

        [Header("COMPONENTS")]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private FloatingCombat floatingCombat;

        [Header("SETTINGS")]
        [SerializeField]
        private int maxHealth = 100;

        [SerializeField]
        private int currentHealth = 100;

        [SerializeField]
        private float rotationSpeedMin = 25f;

        [SerializeField]
        private float rotationSpeedMax = 75f;

        [SerializeField]
        private int pointsValue = 100;

        [Header("AUDIO")]
        [SerializeField]
        private AudioSource hazardDestroySound;

        private Camera mainCamera;

        private Vector2 min;

        public event Action<HazardBase> OnClear;
        public event Action OnKill;
        public event Action<int> OnAddPoints;

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

        private void Update()
        {
            transform.Rotate(0, 0, Random.Range(rotationSpeedMin, rotationSpeedMax) * Time.deltaTime);

            if (currentHealth <= 0)
            {
                OnKill?.Invoke();
                OnAddPoints?.Invoke(PointsValue);

                floatingCombat.ShowCombatText();

                PlayExplosion();
                OnClear?.Invoke(this);
            }
            else if (transform.position.y < min.y)
            {
                OnClear?.Invoke(this);
            }
        }

        private void OnEnable()
        {
            currentHealth = maxHealth;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(GameTag.PlayerBullet))
            {
                StartCoroutine(FlashSprite());

                currentHealth -= 25;
            }
            else if (col.CompareTag(GameTag.PlayerShip))
            {
                currentHealth = 0;
            }

            if (!col.CompareTag(GameTag.Bomb))
            {
                return;
            }

            StartCoroutine(FlashSprite());
            PlayExplosion();

            OnAddPoints?.Invoke(PointsValue);

            OnClear?.Invoke(this);
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
            var explosion = Instantiate(hazardExplosion);
            explosion.transform.position = transform.position;

            if (hazardDestroySound.clip != null)
            {
                hazardDestroySound.Play();
            }
        }
    }
}