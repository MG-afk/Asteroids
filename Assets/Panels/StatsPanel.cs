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

        [UsedImplicitly, Inject]
        public void Construct(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _gameManager.Scored += OnScored;
            _gameManager.LostLife += OnLostLife;
        }

        private void OnDestroy()
        {
            _gameManager.Scored -= OnScored;
            _gameManager.LostLife -= OnLostLife;
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