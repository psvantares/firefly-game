using UnityEngine;

namespace Game
{
    public class EnemyStandardBullet : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField]
        private float shotSpeed;

        private Camera mainCamera;
        private bool isReady;

        private void Awake()
        {
            mainCamera = Camera.main;
            isReady = true;
        }

        private void Update()
        {
            if (!isReady)
            {
                return;
            }

            var tr = transform;
            var pos = tr.position;
            var velocity = new Vector3(0, shotSpeed * Time.deltaTime, 0);
            pos += tr.rotation * velocity;
            tr.position = pos;

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