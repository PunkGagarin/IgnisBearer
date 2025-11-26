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
        [Inject] private AudioService _audioService;

        public async void Enter()
        {
            // show curtain
            // some resource loading
            // asset provider loading etc
            // init all project context systems
            _audioService.Init();
            
            _gameStateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
        }
    }
}