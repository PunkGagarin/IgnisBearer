using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMover : MonoBehaviour
    {
        private Unit _unit;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
        }

        public UniTask MoveTo(Vector3 destination)
        {
            var task = transform.DOMove(destination, _unit.Context.MoveSpeed)
                .SetSpeedBased()
                .SetEase(Ease.Linear)
                .ToUniTask();
            return task;
        }


        public UniTask MoveTo(Vector3 destination, MoveType moveType, CancellationToken cancellationToken)
        {
            var speed = GetSpeedByType(moveType);
            var task = transform.DOMove(destination, _unit.Context.MoveSpeed)
                .SetSpeedBased()
                .SetEase(Ease.Linear)
                .ToUniTask(cancellationToken: cancellationToken)
                .SuppressCancellationThrow();

            return task;
        }

        private float GetSpeedByType(MoveType moveType)
        {
            return moveType switch
            {
                MoveType.Idle => _unit.Context.IdleMoveSpeed,
                MoveType.Run => _unit.Context.MoveSpeed,
                _ => _unit.Context.MoveSpeed
            };
        }
    }

    public enum MoveType
    {
        None = 0,
        Idle = 1,
        Run = 2
    }
}