using _Project.Scripts.Gameplay.Units.Machine;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitIdleState : IState, IUnitState
    {
        [Inject] private UnitSettings _unitSettings;
        
        private UnitStateMachine _unitStateMachine;

        public UnitIdleState(UnitStateMachine unitStateMachine)
        {
            _unitStateMachine = unitStateMachine;
        }

        public void Enter()
        {
            Debug.Log("We are in idle state");
            //start idle animation
        }

        public void Update()
        {
            //choose random time
            //after random time
            //choose random location
            //move to this location
        }

        public void Exit()
        {
        }
    }
}