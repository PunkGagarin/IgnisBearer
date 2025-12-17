using _Project.Scripts.Gameplay.Buildings;
using Zenject;

namespace _Project.Scripts
{
    public class GameEndService
    {
        
        [Inject] private readonly GameEndUI _gameEndUI;
        [Inject] private readonly BuildingsService _buildingsService;

        private IResourceStorage _churchLightStorage;


        public void Init()
        {
            _churchLightStorage = _buildingsService.GetChurch().GetComponent<IResourceStorage>();
            _churchLightStorage.OnReachZero += EndGame;
            _churchLightStorage.OnDestroyed += UnSubscribe;
        }

        private void UnSubscribe()
        {
            _churchLightStorage.OnReachZero -= EndGame;
            _churchLightStorage.OnDestroyed -= UnSubscribe;
        }

        private void EndGame()
        {
            _gameEndUI.Show();
        }
    }
}