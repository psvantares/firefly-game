using UnityEngine;

namespace Game.Components
{
    public class MovingTarget : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField]
        private float moveSpeed = 2.5f;

        [SerializeField]
        private bool dirRight = true;

        private void Update()
        {
            if (dirRight)
            {
                transform.Translate(Vector2.right * (moveSpeed * Time.deltaTime));
            }
            else
            {
                transform.Translate(-Vector2.right * (moveSpeed * Time.deltaTime));
            }

            if (transform.position.x >= 3.0f)
            {
                dirRight = false;
            }

            if (transform.position.x <= -3)
            {
                dirRight = true;
            }
        }
    }
}