using System;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToState : IUnitState, IPayloadState<Vector3>
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

        public async void Enter<TNextState>(Vector3 movePos) where TNextState : class, IState, IUnitState
        {
            _unit.Context.SetUnitStatus(UnitStatus.Busy);
            await _unit.Mover.MoveTo(movePos);
            _unit.StateMachine.Enter<TNextState>();


            Type nextState = typeof(WorkerService);
            _unit.StateMachine.Enter(nextState);
        }

        public void Exit()
        {
        }
    }
}