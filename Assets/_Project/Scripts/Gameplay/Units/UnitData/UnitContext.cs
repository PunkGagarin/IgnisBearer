using System;
using System.Collections.Generic;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitContext
    {
        public event Action<Unit> OnUnitFree = delegate { };
        public event Action<Unit> OnUnitBusy = delegate { };

        public Unit Unit { get; private set; }

        public IUnitStat MoveSpeed { get; set; }
        public float IdleMoveSpeed => MoveSpeed.GetValue() / 2;
        public UnitStatus Status { get; private set; }

        public UnitSpec Spec { get; set; }

        public int LightAmount { get; set; }

        public UnitContext(Unit unit, UnitStat moveSpeed)
        {
            Unit = unit;
            MoveSpeed = moveSpeed;
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