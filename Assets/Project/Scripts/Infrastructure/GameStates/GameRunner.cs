using System.Collections.Generic;
using Project.Scripts.Infrastructure.GameStates.States;
using Zenject;

namespace Project.Scripts.Infrastructure.GameStates
{
    public class GameRunner : IInitializable
    {

        [Inject] private GameStateMachine _stateMachine;
        [Inject] private List<IGameState> _states;

        public void Initialize()
        {
            RegisterAndStart();
        }

        private void RegisterAndStart()
        {
            foreach (var state in _states)
                _stateMachine.Register(state);

            _stateMachine.Enter<BootstrapState>();
        }
    }
}