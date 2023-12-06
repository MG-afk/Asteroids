using UnityEngine;

namespace AsteroidsGame.Meteor
{
    public class MeteorController : BaseController<MeteorController, MeteorData>
    {
        private bool isBig = false;

        public override void AfterSpawn()
        {
            var randomDirection = Random.insideUnitCircle * Data.ThrustForce;
            Rigidbody.AddForce(randomDirection);
        }

        public void AsBig()
        {
            transform.position = Camera.main.GetEdgePosition();
            SetScale(Random.Range(Data.BigMeteor.MinScale, Data.BigMeteor.MaxScale));
            isBig = true;
        }

        public void AsLittle(Vector2 newPosition)
        {
            transform.position = newPosition;
            SetScale(Random.Range(Data.LittleMeteor.MinScale, Data.LittleMeteor.MaxScale));
            isBig = false;
        }

        private void SetScale(float scale)
        {
            transform.localScale = Vector3.one * scale;
            Rigidbody.mass = scale;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out IHitable hitable))
                return;

            if (!hitable.Hit())
                return;

            Hit();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Rigidbody.AddForce(collision.contacts[0].normal * Data.ThrustForce);
        }

        private void BreakIntoTwoPices()
        {
            for (int i = 0; i < 2; i++)
            {
                SpawnerController.Spawn().AsLittle(transform.position);
            }
        }

        public void Destory()
        {
            if (isBig)
            {
                BreakIntoTwoPices();
            }

            SpawnerController.Despawn(this);
        }

        public override bool Hit()
        {
            Destory();

            return true;
        }
    }
}