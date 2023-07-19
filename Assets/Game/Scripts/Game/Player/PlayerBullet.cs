using System;
using UnityEngine;

namespace Game
{
    public class PlayerBullet : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField]
        private float shotSpeed = 6.0f;

        [Header("GAME OBJECTS")]
        [SerializeField]
        private GameObject hitEffect;

        public event Action<PlayerBullet> OnClear;

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            var trs = transform;
            var pos = trs.position;
            var velocity = new Vector3(0, shotSpeed * Time.deltaTime, 0);

            pos += trs.rotation * velocity;
            trs.position = pos;

            Vector2 max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

            if (transform.position.y > max.y)
            {
                OnClear?.Invoke(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag(GameTag.EnemyShip) && !col.CompareTag(GameTag.WorldHazard))
            {
                return;
            }

            PlayHitEffect();
            OnClear?.Invoke(this);
        }

        private void PlayHitEffect()
        {
            var hitSpark = Instantiate(hitEffect);
            hitSpark.transform.position = transform.position;
        }
    }
}