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

        [field: SerializeField]
        public Vector3 GatherPointOffset { get; private set; } = new(3, -3, 0);

        private readonly Queue<Unit> _queue = new();
        private int _capacity;

        public void Init(int capacity)
        {
            ActivateCapacityFor(capacity);
        }

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

        private void ActivateCapacityFor(int capacity)
        {
            _capacity = capacity;

            if (_capacity > Slots.Count)
            {
                Debug.LogError($" Не совпадает количество слотов для инициализации и реальное" +
                               $"Просят: {_capacity}, есть: {Slots.Count}");
                _capacity = Slots.Count;
            }

            for (int i = 0; i < Slots.Count; i++)
                SetSlotActivity(_capacity, i);
        }

        private void SetSlotActivity(int capacity, int index)
        {
            var slot = Slots[index];
            if (index < capacity)
                slot.Activate();
            else
                slot.Deactivate();
        }

        public void SendUnitToQueue(Unit unit)
        {
            unit.Mover.OnReach += DequeueFromMoving;
            unit.StateMachine.Enter<UnitMoveToState, Vector3>(GatherPointPosition());
        }

        private void DequeueFromMoving(Unit unit)
        {
            unit.Mover.OnReach -= DequeueFromMoving;

            var unitInQueueCount = UnitsInQueueWithSlotsCount();
            if (HasNoSpaceInside(unitInQueueCount))
                unit.StateMachine.Enter<UnitMoveToWithNext, UnitWaitState, Vector3>(
                    GetNextPositionForInQueue());
            
            AddUnitForQueue(unit);
        }

        private int UnitsInQueueWithSlotsCount()
        {
            return _queue.Count + GetBusySlotsCount();
        }

        private int GetBusySlotsCount()
        {
            return Slots.Count(el => el.IsBusy);
        }

        private bool HasNoSpaceInside(int unitCount)
        {
            return !HasSpaceInside(unitCount);
        }

        private bool HasSpaceInside(int unitCount)
        {
            return _capacity > unitCount;
        }

        private Vector3 GatherPointPosition()
        {
            return transform.position + GatherPointOffset;
        }

        private Vector3 GetNextPositionForInQueue()
        {
            var unitsCount = UnitsInQueueWithSlotsCount();
            
            return HasSpaceInside(unitsCount)
                ? transform.position
                : GetNextPositionWithOffset(unitsCount - _capacity + 1);
        }

        private Vector3 GetNextPositionWithOffset(int offsetCount)
        {
            var settingsPositionYOffset = -1f * _settings.PositionOffset * offsetCount;
            var offset = new Vector3(0, settingsPositionYOffset, 0);
            return transform.position + offset;
        }

        private void CheckForUnit(ChurchLightSendSlot slot)
        {
            if (!HasUnitsInQueue()) return;

            int index = 0;
            
            foreach (Unit unitInQueue in _queue)
            {
                MoveForwardInQueue(unitInQueue, index);
                index++;
            }

            var unit = _queue.Dequeue();
            SetUnitToSlot(slot, unit);
        }

        private bool HasUnitsInQueue()
        {
            return _queue.Count > 0;
        }

        private void MoveForwardInQueue(Unit unit, int index)
        {
            var posToMove = GetNextPositionWithOffset(index);
            //todo: bug нужен порядковый номер
            unit.StateMachine.Enter<UnitMoveToWithNext, UnitWaitState, Vector3>(posToMove);
        }

        private void SetUnitToSlot(ChurchLightSendSlot slot, Unit unit)
        {
            //todo: roman double slot setBusy
            slot.SetBusy();
            unit.StateMachine
                .Enter<UnitMoveToWithNextAndPayload, UnitSendLightToChurchState,
                    Vector3, ChurchLightSendSlot>(transform.position, slot);
        }

        private void AddUnitForQueue(Unit unit)
        {
            if (TryGetFreeSlot(out var slot))
                SetUnitToSlot(slot, unit);
            else
                _queue.Enqueue(unit);
        }

        private bool TryGetFreeSlot(out ChurchLightSendSlot slot)
        {
            slot = Slots.FirstOrDefault(el => el.IsActive && !el.IsBusy);
            return slot != null;
        }
    }
}