using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchQueue : MonoBehaviour
    {
        [field: SerializeField]
        public List<ChurchLightSendSlot> Slots { get; private set; }

        private readonly Queue<Unit> _queue = new();

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
}