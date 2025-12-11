using System;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitContext
    {
        public float MoveSpeed { get; set; }
        public float IdleMoveSpeed { get; set; }
        public UnitStatus Status { get; set; }
        public UnitSpec Spec { get; set; }
        public float FireUpSpeed { get; set; }
        public int LightAmount { get; set; }

        public UnitContext(float moveSpeed, float fireUpSpeed)
        {
            MoveSpeed = moveSpeed;
            IdleMoveSpeed = moveSpeed / 2f;
            FireUpSpeed = fireUpSpeed;
            Spec = UnitSpec.Worker;
            Status = UnitStatus.Free;
        }
    }
}