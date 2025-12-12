using System;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitContext
    {

        public event Action<Unit> OnUnitFree = delegate { };
        public Unit Unit { get; private set; }
        public float MoveSpeed { get; set; }
        public float IdleMoveSpeed { get; set; }
        public UnitStatus Status { get; private set; }
        public UnitSpec Spec { get; set; }
        public float FireUpSpeed { get; set; }
        public int LightAmount { get; set; }

        public UnitContext(float moveSpeed, float fireUpSpeed, Unit unit)
        {
            Unit = unit;
            MoveSpeed = moveSpeed;
            IdleMoveSpeed = moveSpeed / 2f;
            FireUpSpeed = fireUpSpeed;
            Spec = UnitSpec.Worker;
            Status = UnitStatus.Free;
        }

        public void SetUnitStatus(UnitStatus status)
        {
            if (status == UnitStatus.Free)
                OnUnitFree.Invoke(Unit);
        }
    }
}