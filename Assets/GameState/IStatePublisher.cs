namespace AsteroidsGame.GameState
{
    public interface IStatePublisher
    {
        void Publish<T>() where T : IGameState;
    }
}