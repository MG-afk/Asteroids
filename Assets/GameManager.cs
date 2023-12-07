using AsteroidsGame.GameState;
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
        public event Action<int> Scored;

        private readonly IGameStateMachine _gameStateMachine;

        private readonly ISpawnerController<ShipController> _spawnerShip;
        private readonly ISpawnerController<MeteorController> _spawnerMeteor;

        private int lifes = NumOfLifes;
        private int score = 0;

        private int Score
        {
            set
            {
                if (score == value)
                    return;

                score = value;
                Scored?.Invoke(score);
            }
        }

        public GameManager(
            IGameStateMachine gameStateMachine,
            ISpawnerController<ShipController> spawnerShip,
            ISpawnerController<MeteorController> spawnerMeteor)
        {
            _gameStateMachine = gameStateMachine;
            _spawnerShip = spawnerShip;
            _spawnerMeteor = spawnerMeteor;

            _gameStateMachine.Subscribe<GameResetState>(OnRestart);
            _gameStateMachine.Subscribe<GameStartState>(OnStart);
            _gameStateMachine.Subscribe<GameContinueState>(OnContinue);
        }

        ~GameManager()
        {
            _gameStateMachine.Unsubscribe<GameResetState>(OnRestart);
            _gameStateMachine.Unsubscribe<GameStartState>(OnStart);
            _gameStateMachine.Unsubscribe<GameContinueState>(OnContinue);
        }

        public void Initialize()
        {
            //START GAME
            _gameStateMachine.Publish<GameStartState>();
        }

        public void LoseLife()
        {
            lifes--;

            if (lifes <= 0)
            {
                _gameStateMachine.Publish<GameOverState>();
            }

            LostLife?.Invoke();
        }

        public void AddScore(int addScore)
        {
            Score = score + addScore;
        }

        private void OnRestart()
        {
            Score = 0;
            lifes = NumOfLifes;
            _gameStateMachine.Publish<GameStartState>();
        }

        private void OnContinue()
        {
            if (lifes <= 0)
                return;

            for (int i = 0; i < 10; i++)
            {
                _spawnerMeteor.Spawn().AsBig();
            }
        }

        private void OnStart()
        {
            _spawnerShip.Spawn();

            for (int i = 0; i < 10; i++)
            {
                _spawnerMeteor.Spawn().AsBig();
            }
        }
    }
}
