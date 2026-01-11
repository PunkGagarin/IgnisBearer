using System;
using System.Threading;
using _Project.Scripts.Infrastructure.GameStates;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToWithNextAndPayload : IUnitState, IEnterWithPayloadAndNextPayload<Vector3>
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

        public async void Enter<TNextState, TNextPayload>(Vector3 moveTo, TNextPayload nextPayload)
            where TNextState : class, IPayloadState<TNextPayload>, IUnitState
        {
            _cts = new CancellationTokenSource();
            _unit.Context.SetUnitStatus(UnitStatus.Busy);

            try
            {
                await _unit.Mover
                    .MoveTo(moveTo, cancellationToken: _cts.Token);
                _unit.StateMachine.Enter<TNextState, TNextPayload>(nextPayload);
            }
            catch (Exception e)
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