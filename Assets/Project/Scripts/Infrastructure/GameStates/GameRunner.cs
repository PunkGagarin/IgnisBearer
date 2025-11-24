using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.GameStates
{
    public class GameRunner : MonoBehaviour
    {

        [Inject] private GameStateMachine _stateMachine;
        
        // [Inject] private BootstrapState _bootstrap;
        // [Inject] private LoadSceneState _loadSceneState;
        
        [Inject] private List<IGameState> _states;

        private void Start()
        {
            // _stateMachine.Register(_bootstrap);
            // _stateMachine.Register(_loadSceneState);
            
            foreach (var state in _states)
                _stateMachine.Register(state);
            
            _stateMachine.Enter<BootstrapState>();
        }

    }
}