using System;
using System.Collections;
using Game.Data;
using Game.Utilities.Pool;
using TMPro;
using UnityEngine;

namespace Game.Player
{
    public class PlayerControl : MonoBehaviour
    {
        [Header("TEXTS")]
        [SerializeField]
        private TMP_Text powerUpText;

        [Header("GAME OBJECTS")]
        [SerializeField]
        private GameObject explosion;

        [SerializeField]
        private GameObject shield;

        [Header("PREFABS")]
        [SerializeField]
        private PlayerBullet bulletBlue;

        [SerializeField]
        private PlayerBullet bulletRed;

        [SerializeField]
        private PlayerBullet bulletGreen;

        [Header("TRANSFORMS")]
        [SerializeField]
        private Transform bulletParent;

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

        [Header("AUDIO")]
        [SerializeField]
        private AudioSource laserSound;

        [Header("COMPONENTS")]
        [SerializeField]
        private PlayerHealth playerHealth;

        [SerializeField]
        private Rigidbody2D rigidbody2d;

        [Header("SETTINGS")]
        [SerializeField]
        private float moveSpeed = 15.0f;

        private const float FIRE_RATE = 6f;

        private EntitiesPool<PlayerBullet> bulletBluePool;
        private EntitiesPool<PlayerBullet> bulletRedPool;
        private EntitiesPool<PlayerBullet> bulletGreenPool;

        private Camera mainCamera;

        public event Action<int> OnPlayerHealth;
        public event Action OnGameOver;

        private int weaponId = 1;
        private string powerUp;
        private float powerUpTimer;
        private float timeToFire;
        private bool forceField;

        private void Awake()
        {
            bulletBluePool = new EntitiesPool<PlayerBullet>(bulletBlue, bulletParent);
            bulletRedPool = new EntitiesPool<PlayerBullet>(bulletRed, bulletParent);
            bulletGreenPool = new EntitiesPool<PlayerBullet>(bulletGreen, bulletParent);

            mainCamera = Camera.main;
        }

        private void Update()
        {
            powerUpTimer -= Time.deltaTime;

            if (powerUpTimer < 0)
            {
                powerUpTimer = 0;
                ResetWeapon();
            }

            if (Input.GetMouseButton(0))
            {
                var pos = Input.mousePosition;
                var position = transform.position;
                pos.z = position.z - mainCamera.transform.position.z;
                pos = mainCamera.ScreenToWorldPoint(pos);
                var posShip = new Vector3(pos.x, pos.y + 0.7f, pos.z);
                position = Vector3.Lerp(position, posShip, moveSpeed * Time.deltaTime);
                transform.position = position;
            }

            Vector2 min = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

            max.x -= 0.225f;
            min.x += 0.225f;

            max.y -= 0.285f;
            min.y += 0.285f;

            rigidbody2d.position = new Vector2(Mathf.Clamp(rigidbody2d.position.x, min.x, max.x), Mathf.Clamp(rigidbody2d.position.y, min.y, max.y));

            if (!Input.GetMouseButton(0) || !(Time.time > timeToFire))
            {
                return;
            }

            timeToFire = Time.time + 1f / FIRE_RATE;
            FireWeapon();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(GameTag.EnemyShip) || col.CompareTag(GameTag.EnemyBullet))
            {
                if (!forceField)
                {
                    playerHealth.TakeDamage(10);
                    OnPlayerHealth?.Invoke(playerHealth.CurrentHealth);

                    if (playerHealth.CurrentHealth > 0)
                    {
                        PlayShields(0.1f);
                    }
                    else
                    {
                        PlayExplosion();
                        OnGameOver?.Invoke();
                        gameObject.SetActive(false);
                    }
                }
            }
            else if (col.CompareTag(GameTag.WorldHazard))
            {
                if (!forceField)
                {
                    playerHealth.TakeDamage(100);
                    OnPlayerHealth?.Invoke(playerHealth.CurrentHealth);

                    PlayExplosion();
                    OnGameOver?.Invoke();
                    gameObject.SetActive(false);
                }
            }
            else if (col.CompareTag(GameTag.BluePowerUp))
            {
                powerUp = "DOUBLE WEAPON";
                powerUpTimer = 8f;
                weaponId = 2;
                StartCoroutine(ShowPowerUpText());
            }
            else if (col.CompareTag(GameTag.RedPowerUp))
            {
                powerUp = "TRIPLE WEAPON";
                powerUpTimer = 8f;
                weaponId = 3;
                StartCoroutine(ShowPowerUpText());
            }
            else if (col.CompareTag(GameTag.GreenPowerUp))
            {
                powerUp = "+50 HP";

                playerHealth.GiveHealth(50);
                OnPlayerHealth?.Invoke(playerHealth.CurrentHealth);

                StartCoroutine(ShowPowerUpText());
            }
            else if (col.CompareTag(GameTag.BrownPowerUp))
            {
                powerUp = "SHIELD ON";

                forceField = true;
                PlayShields(10);
                StartCoroutine(ShowPowerUpText());
            }
        }

        public void Initialize()
        {
            gameObject.SetActive(true);
            transform.position = new Vector2(0, -2.5f);
            shield.SetActive(false);
            playerHealth.Initialize();
        }

        public void ResetPowerUp()
        {
            powerUpText.text = "";
        }

        private void FireWeapon()
        {
            laserSound.Play();

            switch (weaponId)
            {
                case 1:
                    var blue = bulletBluePool.Rent();
                    var tr = blue.transform;
                    tr.position = fire1.position;
                    tr.rotation = fire1.rotation;
                    blue.OnClear += HandleBlueClear;
                    break;
                case 2:
                    var green1 = bulletGreenPool.Rent();
                    var tr1 = green1.transform;
                    tr1.position = fire2.position;
                    tr1.rotation = fire2.rotation;
                    green1.OnClear += HandleGreenClear;

                    var green2 = bulletGreenPool.Rent();
                    var tr2 = green2.transform;
                    tr2.position = fire3.position;
                    tr2.rotation = fire3.rotation;
                    green2.OnClear += HandleGreenClear;
                    break;
                case 3:
                    var red1 = bulletRedPool.Rent();
                    var tr3 = red1.transform;
                    tr3.position = fire1.position;
                    tr3.rotation = fire1.rotation;
                    red1.OnClear += HandleRedClear;

                    var red2 = bulletRedPool.Rent();
                    var tr4 = red2.transform;
                    tr4.position = fire4.position;
                    tr4.rotation = fire4.rotation;
                    red2.OnClear += HandleRedClear;

                    var red3 = bulletRedPool.Rent();
                    var tr5 = red3.transform;
                    tr5.position = fire5.position;
                    tr5.rotation = fire5.rotation;
                    red3.OnClear += HandleRedClear;

                    break;
            }
        }

        private IEnumerator ShowPowerUpText()
        {
            powerUpText.text = powerUp;

            yield return new WaitForSeconds(2f);

            ResetPowerUp();
        }

        private void ResetWeapon()
        {
            weaponId = 1;
        }

        private void PlayShields(float duration)
        {
            StartCoroutine(AnimateShields(duration));
        }

        private IEnumerator AnimateShields(float duration)
        {
            shield.SetActive(true);

            yield return new WaitForSeconds(duration);

            forceField = false;
            shield.SetActive(false);
        }

        private void PlayExplosion()
        {
            var instantiate = Instantiate(explosion);
            instantiate.transform.position = transform.position;
        }

        private void HandleBlueClear(PlayerBullet playerBullet)
        {
            playerBullet.OnClear -= HandleBlueClear;
            bulletBluePool.Return(playerBullet);
        }

        private void HandleGreenClear(PlayerBullet playerBullet)
        {
            playerBullet.OnClear -= HandleGreenClear;
            bulletGreenPool.Return(playerBullet);
        }

        private void HandleRedClear(PlayerBullet playerBullet)
        {
            playerBullet.OnClear -= HandleRedClear;
            bulletRedPool.Return(playerBullet);
        }
    }
}