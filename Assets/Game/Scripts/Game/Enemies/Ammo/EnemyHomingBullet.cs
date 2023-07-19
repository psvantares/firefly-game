using UnityEngine;

namespace Game
{
    public class EnemyHomingBullet : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField]
        private float moveSpeed = 2.0f;

        private Camera mainCamera;
        private Vector2 direction;
        private bool isReady;

        private void Awake()
        {
            mainCamera = Camera.main;
            isReady = false;
        }

        public void SetDirection(Vector2 direction)
        {
            this.direction = direction.normalized;
            isReady = true;
        }

        private void Update()
        {
            if (!isReady)
            {
                return;
            }

            var tr = transform;
            Vector2 position = tr.position;
            position += direction * (moveSpeed * Time.deltaTime);
            tr.position = position;

            Vector2 min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

            if (transform.position.x < min.x || transform.position.x > max.x ||
                transform.position.y < min.y || transform.position.y > max.y)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(GameTag.PlayerShip))
            {
                Destroy(gameObject);
            }
        }
    }
}