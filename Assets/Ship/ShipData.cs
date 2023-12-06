using System;
using UnityEngine;

namespace AsteroidsGame.Ship
{
    [Serializable]
    public record ShipData : IData
    {
        [field: SerializeField]
        public ShipController Prefab;

        [field: SerializeField]
        public float ThrustForce { get; private set; }

        [field: SerializeField]
        public float RotationSpeed { get; private set; }

    }
}
