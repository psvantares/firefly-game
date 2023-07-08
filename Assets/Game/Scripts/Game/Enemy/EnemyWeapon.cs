using Game.Data;
using Game.Enemy.Ammo;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyWeapon : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField]
        private bool isHomingWeapon;

        [SerializeField]
        private float attackRate = 0.50f;

        [SerializeField]
        private int bulletCount = 1;

        [SerializeField]
        private Vector3 bulletOffset = new(0, 0.25f, 0);

        [SerializeField]
        private Vector3 bullet2Offset = new(0, 0.25f, 0);

        [Header("GAME OBJECTS")]
        [SerializeField]
        private GameObject enemyBullet;

        private float cooldownTimer;

        private void Update()
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                cooldownTimer = attackRate;

                if (isHomingWeapon)
                {
                    HomingFire();
                }
                else
                {
                    StandardFire();
                }
            }
        }

        private void StandardFire()
        {
            switch (bulletCount)
            {
                case 1:
                {
                    var tr = transform;
                    var rotation = tr.rotation;
                    var offset = rotation * bulletOffset;
                    var bulletGo = Instantiate(enemyBullet, tr.position + offset, rotation);
                    bulletGo.layer = gameObject.layer;
                    break;
                }
                case 2:
                {
                    var tr = transform;
                    var rotation = tr.rotation;
                    var offset = rotation * bulletOffset;
                    var offset2 = rotation * bullet2Offset;
                    var bulletGo = Instantiate(enemyBullet, tr.position + offset, rotation);
                    bulletGo.layer = gameObject.layer;
                    var updTr = transform;
                    var bullet2Go = Instantiate(enemyBullet, updTr.position + offset2, updTr.rotation);
                    bullet2Go.layer = gameObject.layer;
                    break;
                }
            }
        }

        private void HomingFire()
        {
            var playerShip = GameObject.FindWithTag(GameTag.PlayerShip);

            if (playerShip == null)
            {
                return;
            }

            var tr = transform;
            var rotation = tr.rotation;
            var offset = rotation * bulletOffset;
            var bullet = Instantiate(enemyBullet, tr.position + offset, rotation);
            bullet.layer = gameObject.layer;
            Vector2 direction = playerShip.transform.position - bullet.transform.position;
            bullet.GetComponent<EnemyHomingBullet>().SetDirection(direction);
        }
    }
}