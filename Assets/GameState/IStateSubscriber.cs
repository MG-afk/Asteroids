using System;

namespace AsteroidsGame.GameState
{
    public interface IStateSubscriber
    {
        public void Subscribe<T>(Action action) where T : IGameState;
        public void Unsubscribe<T>(Action action) where T : IGameState;
    }
}