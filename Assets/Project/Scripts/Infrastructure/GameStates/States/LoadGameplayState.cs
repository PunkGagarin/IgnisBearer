using Project.Scripts.Infrastructure.SceneManagement;
using Project.Scripts.SceneManagement;
using Zenject;

namespace Project.Scripts.Infrastructure.GameStates.States
{
    public class LoadGameplayState : IState, IGameState
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private GameStateMachine _stateMachine;
        [Inject] private LoadingCurtain _loadingCurtain;

        public async void Enter()
        {
            _loadingCurtain.Show();
            
            //load resources
            await _sceneLoader.LoadScene(SceneEnum.Gameplay);
            //create buildings & units
            
            _loadingCurtain.Hide();
        }

        public void Exit()
        {
            
        }
    }

}