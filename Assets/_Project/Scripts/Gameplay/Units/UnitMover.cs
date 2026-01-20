using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMover : MonoBehaviour
    {
        private Unit _unit;

        private Transform _transform;

        public Action<Unit> OnReach = delegate { };

        private void Awake()
        {
            _unit = GetComponent<Unit>();
            _transform = GetComponent<Transform>();
        }

        private void FlipIfNeeded(Vector3 destination)
        {
            if (destination.x > transform.position.x)
                _transform.localScale = new Vector2(1, _transform.localScale.y);
            else
                _transform.localScale = new Vector2(-1, _transform.localScale.y);
        }


        public UniTask MoveTo(Vector3 destination, MoveType moveType = MoveType.Run, CancellationToken cancellationToken = default)
        {
            //todo: если уничтожаем юнита он продолжает двигаться, надо залинковать внешний токен с монобехом
            var speed = GetSpeedByType(moveType);
            FlipIfNeeded(destination);

            var task = transform.DOMove(destination, speed)
                .SetSpeedBased()
                .SetEase(Ease.Linear)
                .OnComplete(() => OnReach.Invoke(_unit))
                .ToUniTask(cancellationToken: cancellationToken);

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