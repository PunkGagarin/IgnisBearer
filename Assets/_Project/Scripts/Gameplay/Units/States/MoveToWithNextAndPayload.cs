using System;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{

    public class MoveToWithNextAndPayload : IUnitState, IEnterWithPayloadAndNextPayload<Vector3>
    {
        private Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Update()
        {
        }

        public async void Enter<TNextState, TNextPayload>(Vector3 moveTo, TNextPayload nextPayload)
            where TNextState : class, IPayloadState<TNextPayload>, IUnitState
        {
            _unit.Context.SetUnitStatus(UnitStatus.Busy);
            await _unit.Mover.MoveTo(moveTo);
            _unit.StateMachine.Enter<TNextState, TNextPayload>(nextPayload);
        }

        public void Exit()
        {
        }
    }
}