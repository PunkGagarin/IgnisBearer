using Project.Scripts.Infrastructure.SceneManagement;
using Project.Scripts.SceneManagement;
using Zenject;

namespace Project.Scripts.Infrastructure.GameStates.States
{
    public class MainMenuState : IState, IGameState
    {
                
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private LoadingCurtain _loadingCurtain;
        
        public void Exit()
        {
            
        }

        public async void Enter()
        {
            _loadingCurtain.Show();
            await _sceneLoader.LoadScene(SceneEnum.MainMenu);
            _loadingCurtain.Hide();
        }
    }
}