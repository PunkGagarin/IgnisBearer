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


        private readonly Vector3 _gatherPointOffset = new Vector3(3, -3, 0);

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

        public void SendUnitToQueue(Unit unit)
        {
            unit.Mover.OnReach += DequeueFromMoving;

            var nextPositionForFarUnits = GetGatherPoint();

            unit.StateMachine.Enter<UnitMoveToState, Vector3>(
                nextPositionForFarUnits);
        }

        private void DequeueFromMoving(Unit unit)
        {
            unit.Mover.OnReach -= DequeueFromMoving;

            if (_queue.Count + GetBusySlotsCount() >= _capacity)
            {
                unit.StateMachine.Enter<UnitMoveToWithNext, UnitWaitState, Vector3>(
                    GetNextPositionForInQueue());
            }

            AddUnitForQueue(unit);
        }

        private int GetBusySlotsCount()
        {
            return Slots.Count(el => el.IsBusy);
        }

        private Vector3 GetGatherPoint()
        {
            return transform.position + _gatherPointOffset;
        }

        private bool HasSpaceInside(int movingUnitsCount)
        {
            return _capacity > movingUnitsCount;
        }

        private Vector3 GetNextPositionForInQueue()
        {
            var unitsCount = _queue.Count + GetBusySlotsCount();

            var hasSpaceInside = HasSpaceInside(unitsCount);
            var nextPositionForFarUnits = hasSpaceInside
                ? transform.position
                : GetNextPositionWithOffset(unitsCount - _capacity + 1);
            return nextPositionForFarUnits;
        }

        private Vector3 GetNextPositionWithOffset(int offsetCount)
        {
            var settingsPositionYOffset = -1f * _settings.PositionOffset * offsetCount;
            var offset = new Vector3(0, settingsPositionYOffset, 0);
            return transform.position + offset;
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

        private void MoveForwardBy(Unit unit)
        {
            //todo: bug нужен порядковый номер
            unit.StateMachine.Enter<UnitMoveToWithNext, UnitWaitState, Vector3>(
                unit.transform.position + new Vector3(0, _settings.PositionOffset, 0));
        }

        private void AddUnitForQueue(Unit unit)
        {
            if (TryGetFreeSlot(out var slot))
                SetUnitToSlot(slot, unit);
            else
                _queue.Enqueue(unit);
        }

        private void SetUnitToSlot(ChurchLightSendSlot slot, Unit unit)
        {
            slot.SetBusy();
            unit.StateMachine
                .Enter<UnitMoveToWithNextAndPayload, UnitSendLightToChurchState,
                    Vector3, ChurchLightSendSlot>(transform.position, slot);
        }

        private bool TryGetFreeSlot(out ChurchLightSendSlot slot)
        {
            slot = Slots.FirstOrDefault(el => el.IsActive && !el.IsBusy);
            return slot != null;
        }
    }
}