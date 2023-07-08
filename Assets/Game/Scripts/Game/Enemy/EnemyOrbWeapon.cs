using Game.Data;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyOrbWeapon : MonoBehaviour
    {
        [Header("TRANSFORMS")]
        [SerializeField]
        private Transform fire1;

        [SerializeField]
        private Transform fire2;

        [SerializeField]
        private Transform fire3;

        [SerializeField]
        private Transform fire4;

        [SerializeField]
        private Transform fire5;

        [SerializeField]
        private Transform fire6;

        [Header("GAME OBJECTS")]
        [SerializeField]
        private GameObject enemyBullet;

        [Header("AUDIO")]
        [SerializeField]
        private AudioSource fireSound;

        [Header("SETTINGS")]
        [SerializeField]
        private float fireDelay = 0.50f;

        private float cooldownTimer;

        private void Update()
        {
            cooldownTimer -= Time.deltaTime;

            if (!(cooldownTimer <= 0))
            {
                return;
            }

            cooldownTimer = fireDelay;

            FireOrbWeapon();
        }

        private void FireOrbWeapon()
        {
            var playerShip = GameObject.FindWithTag(GameTag.PlayerShip);

            if (playerShip == null)
            {
                return;
            }

            Instantiate(enemyBullet, fire1.position, fire1.rotation);
            Instantiate(enemyBullet, fire2.position, fire2.rotation);
            Instantiate(enemyBullet, fire3.position, fire3.rotation);
            Instantiate(enemyBullet, fire4.position, fire4.rotation);
            Instantiate(enemyBullet, fire5.position, fire5.rotation);
            Instantiate(enemyBullet, fire6.position, fire6.rotation);

            if (fireSound.clip != null)
            {
                fireSound.Play();
            }
        }
    }
}