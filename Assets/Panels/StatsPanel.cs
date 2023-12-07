using AsteroidsGame.GameState;
using JetBrains.Annotations;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AsteroidsGame.Panels
{
    public class StatsPanel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreText;

        [SerializeField]
        private Image[] _hpImages;

        private IGameManager _gameManager;
        private IStateSubscriber _stateSubscriber;

        [UsedImplicitly, Inject]
        public void Construct(
            IGameManager gameManager,
            IStateSubscriber stateSubscriber)
        {
            _gameManager = gameManager;
            _stateSubscriber = stateSubscriber;
            _gameManager.Scored += OnScored;
            _gameManager.LostLife += OnLostLife;

            _stateSubscriber.Subscribe<GameResetState>(OnReset);
        }

        private void OnDestroy()
        {
            _gameManager.Scored -= OnScored;
            _gameManager.LostLife -= OnLostLife;

            _stateSubscriber.Unsubscribe<GameResetState>(OnReset);
        }

        private void OnReset()
        {
            foreach (var hpImg in _hpImages)
            {
                hpImg.gameObject.SetActive(true);
            }
        }

        private void OnScored(int score)
        {
            _scoreText.text = $"Score:{score}";
        }

        private void OnLostLife()
        {
            var image = _hpImages.LastOrDefault(image => image.gameObject.activeSelf);
            image?.gameObject.SetActive(false);
        }
    }
}