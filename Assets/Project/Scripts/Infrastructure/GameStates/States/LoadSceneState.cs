using Project.Scripts.SceneManagement;
using Zenject;

namespace Project.Scripts.Infrastructure.GameStates.States
{
    public class LoadSceneState : IPayloadState<SceneEnum>, IGameState
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private GameStateMachine _stateMachine;

        public async void Enter(SceneEnum sceneEnum)
        {
            await _sceneLoader.LoadScene(sceneEnum);
            // _stateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
            
        }
    }

}