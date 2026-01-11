using System.Threading;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToState : IUnitState, IPayloadState<Vector3>
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

            await _unit.Mover
                .MoveTo(movePos, cancellationToken: _cts.Token).SuppressCancellationThrow();
        }

        public void Exit()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}