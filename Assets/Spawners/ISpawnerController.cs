using UnityEngine;

namespace AsteroidsGame.Spawners
{
    public interface ISpawnerController<TValue> where TValue : MonoBehaviour, ISpawnable
    {
        TValue Spawn();
        void Despawn(TValue value);
    }
}