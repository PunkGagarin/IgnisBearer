using Project.Scripts.Audio;
using Project.Scripts.Infrastructure.SceneManagement;
using Project.Scripts.SceneManagement;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.GameStates.States
{
    public class BootstrapState : IState, IGameState
    {
        
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private AudioService _audioService;
        [Inject] private LoadingCurtain _loadingCurtain;
        

        public async void Enter()
        {
            _loadingCurtain.Show();
            // show curtain
            // some resource loading
            // asset provider loading etc
            // init all project context systems
            _audioService.Init();
            await _sceneLoader.LoadScene(SceneEnum.MainMenu);
            _loadingCurtain.Hide();
            _gameStateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
        }
    }
}