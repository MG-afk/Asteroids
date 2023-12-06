using AsteroidsGame.Meteor;
using AsteroidsGame.Ship;
using AsteroidsGame.Spawners;
using System;
using Zenject;

namespace AsteroidsGame
{
    public class GameManager : IGameManager, IInitializable
    {
        private const int NumOfLifes = 3;

        public event Action LostLife;
        public event Action GameOver;
        public event Action<int> Scored;

        private int lifes = NumOfLifes;
        private int score = 0;

        private readonly ISpawnersCleaner _spawnersCleaner;
        private readonly ISpawnerController<ShipController> _spawnerShip;
        private readonly ISpawnerController<MeteorController> _spawnerMeteor;

        private ShipController _player;
        private int Score
        {
            set
            {
                Score = value;
                Scored?.Invoke(score);
            }
        }

        public GameManager(
            ISpawnersCleaner spawnersCleaner,
            ISpawnerController<ShipController> spawnerShip,
            ISpawnerController<MeteorController> spawnerMeteor)
        {
            _spawnersCleaner = spawnersCleaner;
            _spawnerShip = spawnerShip;
            _spawnerMeteor = spawnerMeteor;
        }

        public void Initialize()
        {
            StartGame();
        }

        private void StartGame()
        {
            _player = _spawnerShip.Spawn();

            for (int i = 0; i < 10; i++)
            {
                _spawnerMeteor.Spawn().AsBig();
            }
        }

        public void LoseLife()
        {
            lifes--;

            if (lifes <= 0)
            {
                GameOver?.Invoke();
                _spawnerShip.Despawn(_player);
            }

            LostLife?.Invoke();
        }

        public void Restart()
        {
            _spawnersCleaner.RemoveAll();

            StartGame();

            score = 0;
            lifes = NumOfLifes;
        }

        public void AddScore(int addScore)
        {
            score += addScore;
        }
    }
}
