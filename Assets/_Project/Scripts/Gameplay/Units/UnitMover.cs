using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMover : MonoBehaviour
    {
        private Unit _unit;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void FlipIfNeeded(Vector3 destination)
        {
            if (destination.x > transform.position.x)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = true;
        }


        public UniTask MoveTo(Vector3 destination, MoveType moveType = MoveType.Run, CancellationToken cancellationToken = default)
        {
            //todo: если уничтожаем юнита он продолжает двигаться, надо залинковать внешний токен с монобехом
            var speed = GetSpeedByType(moveType);
            FlipIfNeeded(destination);
            
            var task = transform.DOMove(destination, speed)
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