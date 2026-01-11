using System;
using System.Threading;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToWithNext : IUnitState, IEnterWithNext<Vector3>
    {
        private Unit _unit;
        private CancellationTokenSource _cts;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter<TNextState>(Vector3 moveTo) where TNextState : class, IState, IUnitState
        {
            _cts = new CancellationTokenSource();
            _unit.Context.SetUnitStatus(UnitStatus.Busy);
            
            try
            {
                await _unit.Mover
                    .MoveTo(moveTo, cancellationToken: _cts.Token);
                _unit.StateMachine.Enter<TNextState>();
            }
            catch (Exception e)
            {
                Debug.LogError("Was canceled (удалю этот лог позже)");
                // ignored
            }
        }

        public void Exit()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        public void Update()
        {
        }
    }
}