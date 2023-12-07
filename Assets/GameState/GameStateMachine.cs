using System;
using System.Collections.Generic;

namespace AsteroidsGame.GameState
{
    public sealed class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, Action> _stateObservers = new();

        public void Subscribe<T>(Action action) where T : IGameState
        {
            var stateType = typeof(T);

            if (_stateObservers.TryGetValue(stateType, out var callback))
            {
                callback += action;
                _stateObservers[stateType] = callback;
            }
            else
            {
                _stateObservers[stateType] = action;
            }
        }

        public void Unsubscribe<T>(Action action) where T : IGameState
        {
            var stateType = typeof(T);

            if (_stateObservers.TryGetValue(stateType, out var callback))
            {
                callback -= action;
                _stateObservers[stateType] = callback;
            }
        }

        public void Publish<T>() where T : IGameState
        {
            var stateType = typeof(T);

            if (_stateObservers.TryGetValue(stateType, out var callback))
            {
                callback?.Invoke();
            }
        }
    }
}
