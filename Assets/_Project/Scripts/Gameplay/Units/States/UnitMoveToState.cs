using System;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToState : IUnitState
    {
        private Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter<TNextState>(Vector3 movePos) where TNextState : class, IState, IUnitState
        {
            _unit.Context.Status = UnitStatus.Busy;
            await _unit.Mover.MoveTo(movePos);
            _unit.StateMachine.Enter<TNextState>();


            Type nextState = typeof(WorkerService);
            _unit.StateMachine.Enter(nextState);
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}