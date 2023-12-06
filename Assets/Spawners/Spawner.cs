using AsteroidsGame.Bullet;
using AsteroidsGame.Meteor;
using AsteroidsGame.Ship;
using System;
using UnityEngine;

namespace AsteroidsGame.Spawners
{
    public class Spawner :
        ISpawnerController<BulletController>,
        ISpawnerController<MeteorController>,
        ISpawnerController<ShipController>,
        ISpawnersCleaner
    {
        public Action<GameObject> Destroyed;

        private readonly BulletController.Pool _bulletPool;
        private readonly MeteorController.Pool _meteorPool;
        private readonly ShipController.Pool _shipPool;

        public Spawner(
            BulletController.Pool bulletPool,
            MeteorController.Pool meteorPool,
            ShipController.Pool shipPool)
        {
            _bulletPool = bulletPool;
            _meteorPool = meteorPool;
            _shipPool = shipPool;
        }

        BulletController ISpawnerController<BulletController>.Spawn()
        {
            return _bulletPool.Spawn();
        }

        MeteorController ISpawnerController<MeteorController>.Spawn()
        {
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
            _meteorPool.Despawn(meteor);
        }

        public void Despawn(ShipController ship)
        {
            _shipPool.Despawn(ship);
        }

        public void RemoveAll()
        {
            _bulletPool.DespawnAll();
            _meteorPool.DespawnAll();
            _shipPool.DespawnAll();
        }
    }
}
