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


        public UnitContext(float moveSpeed, float fireUpSpeed)
        {
            MoveSpeed = moveSpeed;
            IdleMoveSpeed = moveSpeed / 2f;
            FireUpSpeed = fireUpSpeed;
            Spec = UnitSpec.Worker;
            Status = UnitStatus.Free;
        }

    }

    public enum UnitStatus
    {
        None = 0,
        Free = 1,
        Busy = 2
    }

    public enum UnitSpec
    {
        None = 0,
        Worker = 1,
        FireRaiser = 2,
        FireHarvester = 3,
        Prayer = 4
    }
}