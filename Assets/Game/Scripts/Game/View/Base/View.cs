using UnityEngine;

namespace Game.View.Base
{
    public class View : SafeArea
    {
        [Header("ROOT")]
        [SerializeField]
        private GameObject root;

        public void SetActive(bool value)
        {
            if (root.activeSelf == value)
            {
                return;
            }

            root.SetActive(value);
        }
    }
}