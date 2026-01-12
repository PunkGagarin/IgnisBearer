using System;
using System.Threading;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToWithIdleAfterState : IUnitState, IPayloadState<Vector3>
    {
        private Unit _unit;
        private CancellationTokenSource _cts;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Update()
        {
        }

        public async void Enter(Vector3 movePos)
        {
            _cts = new CancellationTokenSource();
            _unit.Context.SetUnitStatus(UnitStatus.Busy);

            try
            {
                await _unit.Mover
                    .MoveTo(movePos, cancellationToken: _cts.Token);
                _unit.StateMachine.Enter<UnitIdleState>();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Exit()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}