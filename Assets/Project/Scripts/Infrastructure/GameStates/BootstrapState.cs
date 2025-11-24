using Project.Scripts.SceneManagement;
using Zenject;

namespace Project.Scripts.Infrastructure.GameStates
{
    public class BootstrapState : IState, IGameState
    {
        
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private SceneLoader _sceneLoader;
        

        public void Enter()
        {
            //some resource loading
            //asset provider loading etc
            _sceneLoader.LoadScene(SceneEnum.MainMenu).Forget();
        }

        public void Exit()
        {
        }
    }
}