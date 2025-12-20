using System;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{

    public class SimpleMoveToState : IUnitState, IPayloadState<Vector3>
    {
        private Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Update()
        {
        }

        public async void Enter(Vector3 movePos)
        {
            _unit.Context.SetUnitStatus(UnitStatus.Busy);
            await _unit.Mover.MoveTo(movePos);
            _unit.StateMachine.Enter<UnitIdleState>();
        }

        public void Exit()
        {
        }
    }

}