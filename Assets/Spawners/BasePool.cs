using System;
using UnityEngine;
using Zenject;

namespace AsteroidsGame.Spawners
{
    public abstract class BasePool<TValue> : MemoryPool<TValue> where TValue : MonoBehaviour, ISpawnable
    {
        private event Action Despowned;

        protected override void OnSpawned(TValue item)
        {
            item.gameObject.SetActive(true);
            item.AfterSpawn();

            Despowned += () =>
            {
                OnDespawned(item);
            };
        }

        protected override void OnDespawned(TValue item)
        {
            item.gameObject.SetActive(false);
        }

        public void DespawnAll()
        {
            Despowned?.Invoke();
        }
    }
}
