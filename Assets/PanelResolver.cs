using AsteroidsGame.Panels;
using UnityEngine;
using Zenject;

namespace AsteroidsGame
{
    public class PanelResolver : MonoInstaller
    {
        [SerializeField]
        private GameOverPanel _gameOverPanel;

        [SerializeField]
        private StatsPanel _statsPanel;

        public override void InstallBindings()
        {
            Container.Resolve<GameOverPanel>();
            Container.Resolve<StatsPanel>();
        }
    }
}