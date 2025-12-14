using System;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitContext
    {

        public event Action<Unit> OnUnitFree = delegate { };
        public event Action<Unit> OnUnitBusy = delegate { };

        public Unit Unit { get; private set; }
        public float MoveSpeed { get; set; }
        public float IdleMoveSpeed { get; set; }
        public UnitStatus Status { get; private set; }
        public UnitSpec Spec { get; set; }
        public float FireUpMultiplier { get; set; }
        public float SendLightMultiplier { get; set; }
        public int LightAmount { get; set; }

        public UnitContext(Unit unit, float moveSpeed, float fireUpMultiplier, float sendLightMultiplier)
        {
            Unit = unit;
            MoveSpeed = moveSpeed;
            IdleMoveSpeed = moveSpeed / 2f;
            FireUpMultiplier = fireUpMultiplier;
            SendLightMultiplier = sendLightMultiplier;
            Spec = UnitSpec.Worker;
            Status = UnitStatus.Free;
        }

        public void SetUnitStatus(UnitStatus status)
        {
            Status = status;

            if (status == UnitStatus.Free)
                OnUnitFree.Invoke(Unit);
            else if (status == UnitStatus.Busy)
                OnUnitBusy.Invoke(Unit);
        }
    }
}