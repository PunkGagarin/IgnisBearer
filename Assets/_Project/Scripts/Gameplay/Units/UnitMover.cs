using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

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
    }
}