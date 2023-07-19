using UnityEngine;

namespace Game
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField]
        private int initializeHealth = 100;

        public int CurrentHealth { get; private set; }

        public void Initialize()
        {
            CurrentHealth = initializeHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }

        public void GiveHealth(int health)
        {
            CurrentHealth += health;
        }
    }
}