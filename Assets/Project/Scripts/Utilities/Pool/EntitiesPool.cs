// Copyright (C) 2022 Geronimo Games - All Rights Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.

using UniRx.Toolkit;
using UnityEngine;

namespace GeronimoGames.Firefly.Utilities
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