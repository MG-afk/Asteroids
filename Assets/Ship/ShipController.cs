using AsteroidsGame.Bullet;
using AsteroidsGame.Spawners;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AsteroidsGame.Ship
{
    public class ShipController : BaseController<ShipController, ShipData>
    {
        private ISpawnerController<BulletController> _bulletSpawner;
        private IGameManager _gameManager;
        private InputActions _inputActions;
        private Vector2 _input;

        public bool IsIndestructible { get; private set; }

        [UsedImplicitly, Inject]
        private void Construct(
            ISpawnerController<BulletController> bulletSpawner,
            IGameManager gameManager,
            InputActions inputActions)
        {
            _bulletSpawner = bulletSpawner;
            _gameManager = gameManager;
            _inputActions = inputActions;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Fire.performed += OnFire;
            _gameManager.LostLife += OnLostLife;
        }

        private void OnDisable()
        {
            _inputActions.Player.Move.performed -= OnMove;
            _inputActions.Player.Fire.performed -= OnFire;
            _inputActions.Disable();
            _gameManager.LostLife -= OnLostLife;
        }

        protected override void Update()
        {
            base.Update();

            if (_input.x == 0)
                return;

            Rotate();
        }

        private void FixedUpdate()
        {
            if (_input.y <= 0)
                return;

            Move();
        }

        private void Move()
        {
            Rigidbody.AddForce(Data.ThrustForce * _input.y * transform.up);
        }

        private void Rotate()
        {
            transform.Rotate(Vector3.back, _input.x * Data.RotationSpeed);
        }

        public override void AfterSpawn()
        {
            transform.position = Vector2.zero;
            Rigidbody.velocity = Vector2.zero;
            _input = Vector2.zero;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _input = context.ReadValue<Vector2>();
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            _bulletSpawner.Spawn().Setup(transform);
        }

        private void OnLostLife()
        {
            StartCoroutine(LoseLifeAnimation());
        }

        private IEnumerator LoseLifeAnimation()
        {
            IsIndestructible = true;

            yield return SpriteRenderer.StartBlinking();

            IsIndestructible = false;
        }

        public override bool Hit()
        {
            if (IsIndestructible)
                return false;

            _gameManager.LoseLife();

            return true;
        }
    }
}