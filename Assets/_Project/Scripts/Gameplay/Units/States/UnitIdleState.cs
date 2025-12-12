using System;
using System.Runtime.CompilerServices;
using System.Threading;
using _Project.Scripts.Infrastructure.GameStates;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitIdleState : IState, IUnitState
    {
        [Inject] private UnitSettings _unitSettings;
        [Inject] private LevelService _levelService;

        private Unit _unit;
        private UnitMover Mover => _unit.Mover;

        private float _currentIdleTime;
        private float _idleTimeCap;
        private Vector3 _nextIdleMovePoint;
        private AsyncLazy _moveTask;
        private CancellationTokenSource _cts;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Enter()
        {
            _cts = new CancellationTokenSource();

            Debug.Log("We are in idle state");
            _unit.Context.SetUnitStatus(UnitStatus.Free);
            _idleTimeCap = GetRandomIdleTime();
        }

        public void Update()
        {
            if (_moveTask != null && _moveTask.Task.Status == UniTaskStatus.Pending)
                return;

            _currentIdleTime += Time.deltaTime;

            if (_currentIdleTime > _idleTimeCap)
                IdleMove().Forget();
        }

        public void Exit()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;

            _currentIdleTime = 0f;
            _idleTimeCap = 0f;
            _nextIdleMovePoint = Vector3.zero;
        }

        private async UniTaskVoid IdleMove()
        {
            _nextIdleMovePoint = GetRandomMovePoint();

            _moveTask = Mover.MoveTo(_nextIdleMovePoint, MoveType.Idle, _cts.Token).ToAsyncLazy();
            await _moveTask.Task;

            _currentIdleTime = 0f;
            _idleTimeCap = GetRandomIdleTime();
        }

        private float GetRandomIdleTime()
        {
            return Random.Range(_unitSettings.IdleBeforeMoveMinTime, _unitSettings.IdleBeforeMoveMaxTime);
        }

        private Vector3 GetRandomMovePoint()
        {
            return _levelService.GetRandomMapPosition();
        }

        private async UniTaskVoid Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var idleTime = GetRandomIdleTime();
                await UniTask.Delay(TimeSpan.FromSeconds(idleTime), cancellationToken: token);

                var point = GetRandomMovePoint();
                await Mover.MoveTo(point, MoveType.Idle, token);
            }
        }
    }
}