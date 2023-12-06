using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AsteroidsGame.Panels
{
    public class GameOverPanel : MonoBehaviour, ILateDisposable
    {
        [SerializeField]
        private Button _restartButton;

        private IGameManager _gameManager;

        [UsedImplicitly, Inject]
        public void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _gameManager.GameOver += OnGameOver;
        }

        public void LateDispose()
        {
            _gameManager.GameOver -= OnGameOver;
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(Restart);
        }

        private void Restart()
        {
            _gameManager.Restart();
            gameObject.SetActive(false);
        }

        private void OnGameOver()
        {
            gameObject.SetActive(true);
        }
    }
}