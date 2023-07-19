using UnityEngine;

namespace Game.Components
{
    public class PowerUpControl : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField]
        private float moveSpeed = 0.5f;

        [SerializeField]
        private bool isMoving;

        private Vector2 min;
        private Vector2 max;

        private void Awake()
        {
            isMoving = true;

            var mainCamera = Camera.main;

            if (mainCamera == null)
            {
                return;
            }

            min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

            max.y += GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
            min.y -= GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
        }

        private void Update()
        {
            if (!isMoving)
            {
                return;
            }

            var trs = transform;
            Vector2 position = trs.position;
            position = new Vector2(position.x, position.y + -moveSpeed * Time.deltaTime);
            trs.position = position;

            if (!(transform.position.y < min.y))
            {
                return;
            }

            isMoving = false;
            Destroy(gameObject);
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