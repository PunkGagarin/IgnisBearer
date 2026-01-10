using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchQueue : MonoBehaviour
    {
        [Inject] private readonly ChurchSettings _settings;

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

        public void Init(int capacity)
        {
            ActivateCapacityFor(capacity);
        }

        private void ActivateCapacityFor(int capacity)
        {
            if (capacity > Slots.Count)
            {
                Debug.LogError($" Не совпадает количество слотов для инициализации и реальное" +
                               $"Просят: {capacity}, есть: {Slots.Count}");
                capacity = Slots.Count;
            }

            for (int i = 0; i < Slots.Count; i++)
                SetSlotActivity(capacity, i);
        }

        private void SetSlotActivity(int capacity, int i)
        {
            var slot = Slots[i];
            if (i < capacity)
                slot.Activate();
            else
                slot.Deactivate();
        }

        private void CheckForUnit(ChurchLightSendSlot slot)
        {
            if (_queue.Count > 0)
            {
                foreach (var unitInQueue in _queue)
                    MoveForwardBy(unitInQueue);

                var unit = _queue.Dequeue();
                SetUnitToSlot(slot, unit);
            }
        }

        private void MoveForwardBy(Unit unitInQueue)
        {
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