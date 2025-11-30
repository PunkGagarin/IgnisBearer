using _Project.Scripts.Gameplay.Units.Machine;
using _Project.Scripts.Infrastructure.GameStates;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToHouseState : IState, IUnitState
    {
        private Unit _unit;
        private UnitStateMachine _unitStateMachine;

        public UnitMoveToHouseState(UnitStateMachine unitStateMachine)
        {
        }

        public async void Enter()
        {
            await _unit.Mover.MoveTo(Vector3.zero);
            // _unit.DoThis();
        }

        public void Exit()
        {
        }

        public void Update()
        {

        }
    }
}