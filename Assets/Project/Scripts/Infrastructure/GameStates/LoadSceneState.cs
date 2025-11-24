using Project.Scripts.SceneManagement;
using Zenject;

namespace Project.Scripts.Infrastructure.GameStates
{
    public class LoadSceneState : IPayloadState<SceneEnum>, IGameState
    {
        [Inject] private SceneLoader _sceneLoader;

        public async void Enter(SceneEnum sceneEnum)
        {
            _sceneLoader.LoadScene(sceneEnum).Forget();
        }

        public void Exit()
        {
        }
    }
}