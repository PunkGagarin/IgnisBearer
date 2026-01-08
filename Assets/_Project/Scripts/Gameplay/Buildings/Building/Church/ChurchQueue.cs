using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Ui;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchQueue : MonoBehaviour
    {
        [field: SerializeField]
        public List<ChurchLightSendSlot> Slots { get; private set; }

        private Queue<Unit> _queue = new Queue<Unit>();

        private void Awake()
        {
            foreach (var slot in Slots)
                slot.OnFree += CheckForUnit;
        }

        private void OnDestroy()
        {
            foreach (var slot in Slots)
                slot.OnFree -= CheckForUnit;
        }

        private void CheckForUnit(ChurchLightSendSlot slot)
        {
            if (_queue.Count > 0)
            {
                var unit = _queue.Dequeue();
                SetUnitToSlot(slot, unit);
            }
        }

        public void AddUnitForQueue(Unit unit)
        {
            if (TryGetFreeSlot(out var slot))
                SetUnitToSlot(slot, unit);
            else
            {
                // первое нарушение? не юнит решает, а этот класс?
                unit.StateMachine.Enter<UnitWaitState>();
                _queue.Enqueue(unit);
            }
        }

        private void SetUnitToSlot(ChurchLightSendSlot slot, Unit unit)
        {
            unit.StateMachine.Enter<UnitSendLightToChurchState, ChurchLightSendSlot>(slot);
        }

        private bool TryGetFreeSlot(out ChurchLightSendSlot slot)
        {
            slot = Slots.FirstOrDefault(el => el.IsActive && !el.IsBusy);
            return slot != null;
        }
    }

    public class ChurchLightSendSlot : MonoBehaviour
    {
        [field: SerializeField]
        public BarUi BarUi { get; private set; }


        public bool IsActive { get; private set; }
        public bool IsBusy { get; private set; }
        public Action<ChurchLightSendSlot> OnFree = delegate { };

        public void SetBusy()
        {
            IsBusy = true;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Progress(float progress)
        {
            BarUi.ChangeBarProgress(progress);
        }

        public void SetFree()
        {
            IsBusy = false;
            OnFree.Invoke(this);
        }
    }
}