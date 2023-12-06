using System;
using UnityEngine;

namespace AsteroidsGame.Bullet
{
    [Serializable]
    public record BulletData : IData
    {
        [field: SerializeField]
        public BulletController Prefab;

        [field: SerializeField]
        public float ThrustForce { get; private set; }

        [field: SerializeField]
        public int TimeToDisappear { get; internal set; }

    }
}
