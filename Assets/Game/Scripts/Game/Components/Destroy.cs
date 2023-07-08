using UnityEngine;

namespace Game.Components
{
    public class Destroy : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField]
        private float destroyTime = 0.5f;

        private void Start()
        {
            if (destroyTime != 0)
            {
                Invoke(nameof(DestroyGameObject), destroyTime);
            }
        }

        private void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}