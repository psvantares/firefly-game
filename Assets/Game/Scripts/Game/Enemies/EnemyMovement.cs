using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField]
        private bool chasePlayer;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float turnSpeed = 45f;

        private Transform player;
        private Transform movingTarget;

        private void Start()
        {
            moveSpeed += Random.Range(0.1f, 0.9f);
        }

        private void Update()
        {
            if (chasePlayer)
            {
                if (player == null)
                {
                    var go = GameObject.FindGameObjectWithTag(GameTag.PlayerShip);

                    if (go != null)
                    {
                        player = go.transform;
                    }
                }

                if (player == null)
                {
                    return;
                }
            }
            else
            {
                if (movingTarget == null)
                {
                    var go = GameObject.FindGameObjectWithTag(GameTag.MovingTarget);

                    if (go != null)
                    {
                        movingTarget = go.transform;
                    }
                }

                if (movingTarget == null)
                {
                    return;
                }
            }

            Invoke(chasePlayer ? nameof(StartChasingPlayer) : nameof(StartChasingTarget), 0f);
        }

        private void FacePlayer()
        {
            var dir = player.position - transform.position;
            dir.Normalize();

            var zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            var desiredRot = Quaternion.Euler(0, 0, zAngle);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, turnSpeed * Time.deltaTime);
        }

        private void FaceTarget()
        {
            var dir = movingTarget.position - transform.position;
            dir.Normalize();

            var zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            var desiredRot = Quaternion.Euler(0, 0, zAngle);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, turnSpeed * Time.deltaTime);
        }

        private void StartChasingPlayer()
        {
            FacePlayer();

            var tr = transform;
            var pos = tr.position;
            var velocity = new Vector3(0, 1 * moveSpeed * Time.deltaTime, 0);

            pos += tr.rotation * velocity;
            tr.position = pos;
        }

        private void StartChasingTarget()
        {
            FaceTarget();

            var tr = transform;
            var pos = tr.position;
            var velocity = new Vector3(0, 1 * moveSpeed * Time.deltaTime, 0);

            pos += tr.rotation * velocity;
            tr.position = pos;
        }
    }
}