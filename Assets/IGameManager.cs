using System;

namespace AsteroidsGame
{
    public interface IGameManager
    {
        event Action GameOver;
        event Action LostLife;
        event Action<int> Scored;

        void Restart();
        void LoseLife();
        void AddScore(int score);
    }
}