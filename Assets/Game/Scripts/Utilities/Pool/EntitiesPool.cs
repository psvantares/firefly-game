using UniRx.Toolkit;
using UnityEngine;

namespace Game.Utilities.Pool
{
    public class EntitiesPool<T> : ObjectPool<T> where T : Component
    {
        private readonly T entity;
        private readonly Transform parent;

        public EntitiesPool(T entity, Transform parent = null)
        {
            this.entity = entity;
            this.parent = parent;
        }

        protected override T CreateInstance()
        {
            var instance = Object.Instantiate(entity);

            if (parent != null)
            {
                instance.transform.SetParent(parent, false);
            }

            return instance;
        }

        protected override void OnBeforeRent(T instance)
        {
            base.OnBeforeRent(instance);
            instance.transform.localScale = Vector3.one;
        }
    }
}