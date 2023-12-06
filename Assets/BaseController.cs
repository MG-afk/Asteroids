using AsteroidsGame.Spawners;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace AsteroidsGame
{
    public abstract class BaseController<T, TData> :
        MonoBehaviour, IHitable, ISpawnable
        where T : MonoBehaviour, ISpawnable
        where TData : IData
    {
        public class Pool : BasePool<T> { }

        [SerializeField]
        private Rigidbody2D _rigibody;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private ISpawnerController<T> _spawnerController;
        private TData _data;

        protected Rigidbody2D Rigidbody => _rigibody;
        protected SpriteRenderer SpriteRenderer => _spriteRenderer;
        protected ISpawnerController<T> SpawnerController => _spawnerController;
        protected TData Data => _data;

        [UsedImplicitly, Inject]
        private void Construct(
            ISpawnerController<T> spawnerController,
            TData data)
        {
            _spawnerController = spawnerController;
            _data = data;
        }

        protected virtual void Update()
        {
            transform.KeepObjectOnCamera(_spriteRenderer);
        }

        public abstract bool Hit();

        public abstract void AfterSpawn();
    }
}
