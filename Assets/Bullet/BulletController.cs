using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using Zenject;

namespace AsteroidsGame.Bullet
{
    public sealed class BulletController : BaseController<BulletController, BulletData>
    {
        private IGameManager _gameManager;

        public bool IsPlayer { get; private set; }

        [UsedImplicitly, Inject]
        private void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public override void AfterSpawn()
        {
            StartCoroutine(LiveLongLife(Data.TimeToDisappear));
        }

        private IEnumerator LiveLongLife(float time)
        {
            yield return new WaitForSeconds(time);

            SpawnerController.Despawn(this);
        }

        public void Setup(Transform shooterTransform, bool isPlayer = true)
        {
            transform.position = shooterTransform.position + shooterTransform.up;
            Rigidbody.AddForce(shooterTransform.up * Data.ThrustForce);
            IsPlayer = isPlayer;
        }

        public override bool Hit()
        {
            SpawnerController.Despawn(this);

            if (!IsPlayer)
                return true;

            _gameManager.AddScore(100);

            return true;
        }
    }
}