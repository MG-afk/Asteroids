using AsteroidsGame.Bullet;
using AsteroidsGame.Meteor;
using AsteroidsGame.Ship;
using AsteroidsGame.Spawners;
using UnityEngine;
using Zenject;

namespace AsteroidsGame
{
    [CreateAssetMenu(menuName = "Asteroids/ProjectInstaller")]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private BulletData _bulletData;

        [SerializeField]
        private MeteorData _meteorData;

        [SerializeField]
        private ShipData _shipData;

        public override void InstallBindings()
        {
            InstallBindings(Container);
        }

        public void InstallBindings(DiContainer container)
        {
            BindMemoryPools(container);
            BindData(container);

            container.Bind<InputActions>().AsSingle();

            container.BindInterfacesAndSelfTo<Spawner>().AsSingle();
            container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        }

        private void BindMemoryPools(DiContainer container)
        {
            var parent = new GameObject("SpawnedObjects").transform;

            container.BindMemoryPool<BulletController, BulletController.Pool>()
                .WithMaxSize(100)
                .FromComponentInNewPrefab(_bulletData.Prefab)
                .UnderTransform(parent);

            container.BindMemoryPool<MeteorController, MeteorController.Pool>()
                .WithMaxSize(40)
                .FromComponentInNewPrefab(_meteorData.Prefab)
                .UnderTransform(parent);

            container.BindMemoryPool<ShipController, ShipController.Pool>()
                .WithMaxSize(1)
                .FromComponentInNewPrefab(_shipData.Prefab)
                .UnderTransform(parent);
        }

        private void BindData(DiContainer container)
        {
            container.BindInstance(_bulletData);
            container.BindInstance(_meteorData);
            container.BindInstance(_shipData);
        }
    }
}
