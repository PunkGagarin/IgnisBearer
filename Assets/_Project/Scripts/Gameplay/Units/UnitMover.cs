using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMover : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int LastX = Animator.StringToHash("LastX");
        private static readonly int LastY = Animator.StringToHash("LastY");
        private static readonly int CurX = Animator.StringToHash("CurX");
        private static readonly int CurY = Animator.StringToHash("CurY");
        
        [field:SerializeField] private Animator _animator;
        
        [SerializeField] private Vector2 lightOffsetRight = new(1f, 0.0f);
        [SerializeField] private Vector2 lightOffsetLeft  = new(-1f, 0.0f);
        
        private Unit _unit;

        public Action<Unit> OnReach = delegate { };

        private void Awake()
        {
            _unit = GetComponent<Unit>();
        }
        
        private void UpdateLightDirection(Vector2 dir)
        {
            dir.Normalize(); 
            // todo need to recheck after import final art

            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                _unit.Light2D.transform.localPosition = dir.x > 0 ? lightOffsetRight : lightOffsetLeft;
            }
        }

        public UniTask MoveTo(Vector3 destination, MoveType moveType = MoveType.Run, CancellationToken cancellationToken = default)
        {
            //todo: если уничтожаем юнита он продолжает двигаться, надо залинковать внешний токен с монобехом
            var speed = GetSpeedByType(moveType);
            
            
            _animator.SetBool(IsMoving, true);
            var dir = (destination - transform.position).normalized;
            var x = dir.x;
            var y = dir.y;
            UpdateLightDirection(dir);

            var task = transform.DOMove(destination, speed)
                .SetSpeedBased()
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .OnUpdate(() =>
                {
                    if (_animator == null) return;
                    _animator.SetFloat(CurX, x);
                    _animator.SetFloat(CurY, y);
                })
                .OnComplete(() =>
                {
                    if (_animator == null) return;
                    _animator.SetFloat(LastX, x);
                    _animator.SetFloat(LastY, y);
                    _animator.SetBool(IsMoving, false);
                    OnReach.Invoke(_unit);
                })
                .ToUniTask(cancellationToken: cancellationToken);

            return task;
        }

        private float GetSpeedByType(MoveType moveType)
        {
            return moveType switch
            {
                MoveType.Idle => _unit.Context.IdleMoveSpeed,
                MoveType.Run => _unit.Context.MoveSpeed.GetValue(),
                _ => _unit.Context.MoveSpeed.GetValue()
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