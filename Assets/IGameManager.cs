using System;

namespace AsteroidsGame
{
    public interface IGameManager
    {
        event Action LostLife;
        event Action<int> Scored;

        void LoseLife();
        void AddScore(int score);
    }
}