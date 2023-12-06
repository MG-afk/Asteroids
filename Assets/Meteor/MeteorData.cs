using System;
using UnityEngine;

namespace AsteroidsGame.Meteor
{
    [Serializable]
    public record MeteorData : IData
    {
        [field: SerializeField]
        public MeteorController Prefab;

        [field: SerializeField]
        public float ThrustForce { get; private set; }

        [field: SerializeField]
        public Details BigMeteor { get; private set; }

        [field: SerializeField]
        public Details LittleMeteor { get; private set; }


        [Serializable]
        public record Details
        {
            [field: SerializeField]
            public float MinScale { get; private set; }

            [field: SerializeField]
            public float MaxScale { get; private set; }
        }
    }
}
