using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToState : IUnitState, IPayloadState<Vector3>
    {
        [Inject] BuildingSlotsService _buildingSlotsService;
        private Unit _unit;
        private IUnitState _nextState;


        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter(Vector3 movePosition)
        {
            _unit.Context.Status = UnitStatus.Busy;
            await _unit.Mover.MoveTo(movePosition);
            // _unit.StateMachine.Enter(_nextState);
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }
}