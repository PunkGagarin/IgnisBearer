using System;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToWithNext : IUnitState, IEnterWithNext<Vector3>
    {
        private Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter<TNextState>(Vector3 moveTo) where TNextState : class, IState, IUnitState
        {
            _unit.Context.SetUnitStatus(UnitStatus.Busy);
            await _unit.Mover.MoveTo(moveTo);
            _unit.StateMachine.Enter<TNextState>();
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }
}