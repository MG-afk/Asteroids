using AsteroidsGame.GameState;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AsteroidsGame.Panels
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _restartButton;

        private IStateSubscriber _stateSubscriber;
        private IStatePublisher _statePublisher;

        [UsedImplicitly, Inject]
        public void Construct(
            IStateSubscriber stateSubscriber,
            IStatePublisher statePublisher)
        {
            _stateSubscriber = stateSubscriber;
            _statePublisher = statePublisher;

            _stateSubscriber.Subscribe<GameOverState>(OnGameOver);
        }

        private void OnDestroy()
        {
            _stateSubscriber.Unsubscribe<GameOverState>(OnGameOver);
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
            _statePublisher.Publish<GameResetState>();
            gameObject.SetActive(false);
        }

        private void OnGameOver()
        {
            gameObject.SetActive(true);
        }
    }
}