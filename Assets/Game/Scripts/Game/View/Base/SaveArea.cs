using UnityEngine;

namespace Game.View.Base
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField]
        protected bool safeAreaEnabled;

        private RectTransform RectT => transform as RectTransform;

        protected virtual void Start()
        {
            AdjustSaveAreaScreen();
        }

        private void AdjustSaveAreaScreen()
        {
            if (!safeAreaEnabled)
            {
                return;
            }

            var safeArea = Screen.safeArea;
            var yMax = safeArea.yMax;
            var topAreaHeightInPixels = Screen.height - yMax;
            var scale = 1f / GetCanvas(transform).scaleFactor;
            var topOffset = topAreaHeightInPixels * scale;

            RectT.offsetMax = new Vector2(RectT.offsetMax.x, -topOffset);
        }

        private static Canvas GetCanvas(Transform transform)
        {
            while (true)
            {
                if (transform == null)
                {
                    return null;
                }

                var canvas = transform.GetComponent<Canvas>();

                if (canvas != null)
                {
                    return canvas;
                }

                transform = transform.parent;
            }
        }
    }
}