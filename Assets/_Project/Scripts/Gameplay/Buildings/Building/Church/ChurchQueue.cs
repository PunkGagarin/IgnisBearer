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
        private readonly HashSet<Unit> _movingUnits = new();

        private int _currentCapacity;

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

        public void SendUnitToQueue(Unit unit)
        {
            unit.StateMachine.Enter<UnitMoveToState, Vector3>(
                GetNextPositionForFarUnits());
            unit.Mover.OnReach += DequeueFromMoving;
            _movingUnits.Add(unit);
        }

        private void DequeueFromMoving(Unit unit)
        {
            unit.Mover.OnReach -= DequeueFromMoving;
            _movingUnits.Remove(unit);

            if (_queue.Count > _currentCapacity)
            {
                unit.StateMachine.Enter<UnitMoveToWithNext, UnitWaitState, Vector3>(
                    GetNextPositionForInQueue());
            }
            else
            {
                AddUnitForQueue(unit);
            }
        }

        private Vector3 GetNextPositionForFarUnits()
        {
            var movingUnitsCount = _queue.Count + _movingUnits.Count;

            return _currentCapacity < movingUnitsCount
                ? transform.position
                : GetNextPositionWithOffset(movingUnitsCount - _currentCapacity);
        }

        private Vector3 GetNextPositionForInQueue()
        {
            var unitsCount = _queue.Count;

            return _currentCapacity < unitsCount
                ? transform.position
                : GetNextPositionWithOffset(unitsCount - _currentCapacity);
        }

        private Vector3 GetNextPositionWithOffset(int offsetCount)
        {
            var settingsPositionYOffset = -1f * _settings.PositionOffset * offsetCount;
            var offset = new Vector3(0, settingsPositionYOffset, 0);
            return transform.position + offset;
        }

        private void ActivateCapacityFor(int capacity)
        {
            _currentCapacity = capacity;

            if (_currentCapacity > Slots.Count)
            {
                Debug.LogError($" Не совпадает количество слотов для инициализации и реальное" +
                               $"Просят: {_currentCapacity}, есть: {Slots.Count}");
                _currentCapacity = Slots.Count;
            }

            for (int i = 0; i < Slots.Count; i++)
                SetSlotActivity(_currentCapacity, i);
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

        private void AddUnitForQueue(Unit unit)
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