using AsteroidsGame.Bullet;
using AsteroidsGame.GameState;
using AsteroidsGame.Meteor;
using AsteroidsGame.Ship;
using System;
using UnityEngine;

namespace AsteroidsGame.Spawners
{
    public class Spawner :
        ISpawnerController<BulletController>,
        ISpawnerController<MeteorController>,
        ISpawnerController<ShipController>
    {
        public Action<GameObject> Destroyed;

        private readonly BulletController.Pool _bulletPool;
        private readonly MeteorController.Pool _meteorPool;
        private readonly ShipController.Pool _shipPool;
        private readonly IStateSubscriber _stateSubscriber;
        private readonly IStatePublisher _statePublisher;

        private int _spawnMeteorCount;

        public Spawner(
            BulletController.Pool bulletPool,
            MeteorController.Pool meteorPool,
            ShipController.Pool shipPool,
            IStateSubscriber stateSubscriber,
            IStatePublisher statePublisher)
        {
            _bulletPool = bulletPool;
            _meteorPool = meteorPool;
            _shipPool = shipPool;
            _stateSubscriber = stateSubscriber;
            _statePublisher = statePublisher;

            _stateSubscriber.Subscribe<GameResetState>(OnRestart);
            _stateSubscriber.Subscribe<GameOverState>(OnGameOver);
            _stateSubscriber.Subscribe<GameContinueState>(OnContinue);
        }

        ~Spawner()
        {
            _stateSubscriber.Unsubscribe<GameResetState>(OnRestart);
            _stateSubscriber.Unsubscribe<GameOverState>(OnGameOver);
            _stateSubscriber.Unsubscribe<GameContinueState>(OnContinue);
        }

        private void OnContinue()
        {
            _bulletPool.DespawnAll();
        }

        private void OnGameOver()
        {
            _shipPool.DespawnAll();
        }

        private void OnRestart()
        {
            _spawnMeteorCount = 0;

            _bulletPool.DespawnAll();
            _meteorPool.DespawnAll();
            _shipPool.DespawnAll();
        }

        BulletController ISpawnerController<BulletController>.Spawn()
        {
            return _bulletPool.Spawn();
        }

        MeteorController ISpawnerController<MeteorController>.Spawn()
        {
            _spawnMeteorCount++;
            return _meteorPool.Spawn();
        }

        ShipController ISpawnerController<ShipController>.Spawn()
        {
            return _shipPool.Spawn();
        }

        public void Despawn(BulletController bullet)
        {
            _bulletPool.Despawn(bullet);
        }

        public void Despawn(MeteorController meteor)
        {
            _spawnMeteorCount--;

            if (_spawnMeteorCount == 0)
            {
                _statePublisher.Publish<GameContinueState>();
            }

            _meteorPool.Despawn(meteor);
        }

        public void Despawn(ShipController ship)
        {
            _shipPool.Despawn(ship);
        }
    }
}
