using AsteroidsGame;
using AsteroidsGame.Bullet;
using AsteroidsGame.Meteor;
using AsteroidsGame.Ship;
using AsteroidsGame.Spawners;
using NUnit.Framework;
using UnityEngine;
using Zenject;

[TestFixture]
public class ProjectTest : ZenjectUnitTestFixture
{
    public override void Setup()
    {
        base.Setup();

        var projectInstaller = Resources.Load<ScriptableObject>("ProjectInstaller") as ProjectInstaller;
        projectInstaller.InstallBindings(Container);
    }

    [Test]
    public void GameManager_ShouldEndGameWhenLosingAllLives()
    {
        var gameManager = Container.Resolve<IGameManager>();
        var spawnerController = Container.Resolve<ISpawnerController<ShipController>>();
        var shipController = spawnerController.Spawn();

        gameManager.LoseLife();
        gameManager.LoseLife();
        gameManager.LoseLife();

        Assert.IsNull(shipController);
    }

    [Test]
    public void SpawnBullet_WhenCalled_ShouldSpawnBullet()
    {
        var spawner = Container.Resolve<ISpawnerController<BulletController>>();

        var bullet = spawner.Spawn();

        Assert.IsNotNull(bullet);
        Assert.That(bullet, Is.TypeOf<BulletController>());
    }

    [Test]
    public void DespawnBullet_WhenCalled_ShouldDespawnBullet()
    {
        var spawner = Container.Resolve<ISpawnerController<BulletController>>();

        var bullet = spawner.Spawn();
        spawner.Despawn(bullet);

        Assert.IsNotNull(bullet);
        Assert.That(bullet.gameObject.activeSelf, Is.False);
    }

    [Test]
    public void SpawnMeteor_WhenCalled_ShouldSpawnMeteor()
    {
        var spawner = Container.Resolve<ISpawnerController<MeteorController>>();

        var meteor = spawner.Spawn();

        Assert.IsNotNull(meteor);
        Assert.That(meteor, Is.TypeOf<MeteorController>());
    }

    [Test]
    public void DespawMeteor_WhenCalled_ShouldDespawnMeteor()
    {
        var spawner = Container.Resolve<ISpawnerController<MeteorController>>();

        var meteor = spawner.Spawn();
        spawner.Despawn(meteor);

        Assert.IsNotNull(meteor);
        Assert.That(meteor.gameObject.activeSelf, Is.False);
    }

    [Test]
    public void SpawnShip_WhenCalled_ShouldSpawnShip()
    {
        var spawner = Container.Resolve<ISpawnerController<ShipController>>();

        var ship = spawner.Spawn();

        Assert.IsNotNull(ship);
        Assert.That(ship, Is.TypeOf<ShipController>());
    }

    [Test]
    public void DespawnShip_WhenCalled_ShouldDespawnShip()
    {
        var spawner = Container.Resolve<ISpawnerController<ShipController>>();

        var ship = spawner.Spawn();
        spawner.Despawn(ship);

        Assert.IsNotNull(ship);
        Assert.That(ship.gameObject.activeSelf, Is.False);
    }
}
