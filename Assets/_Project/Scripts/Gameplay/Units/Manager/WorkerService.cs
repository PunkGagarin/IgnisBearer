using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class WorkerService
    {
        public event Action OnWorkerListUpdated;
        public event Action OnFreeUnitAvailable = delegate { };
        public event Action OnAllUnitsBusy = delegate { };

        [Inject] private UnitFactory _factory;

        private List<Unit> _units = new();

        public void CreateStartUnit(UnitSpawnPoint unitPosition)
        {
            Debug.Log(" CreateStartUnit");
            CreateAndRegisterUnit(unitPosition.gameObject.transform);
        }

        public void CreateAndRegisterUnit(Transform unitPosition)
        {
            var unit = _factory.CreateAndInstantiateUnit(unitPosition);
            RegisterUnit(unit);
        }

        //todo: move out to another class
        public bool MoveFreeUnit(Lantern lantern)
        {
            var unit = FindFirstFreeWorker();
            if (unit == null)
                return false;

            unit.StateMachine.Enter<UnitMoveToLanternState, Lantern>(lantern);
            return true;
        }

        private Unit FindFirstFreeWorker()
        {
            if (_units.Count <= 0)
            {
                Debug.LogWarning("Trying to find free worker when there are no workers");
                return null;
            }
            return _units.FirstOrDefault(unit => unit.Context.Status == UnitStatus.Free);
        }

        public void RegisterUnit(Unit unit)
        {
            AddUnitWithSubscription(unit);
            OnWorkerListUpdated?.Invoke();
        }

        private void AddUnitWithSubscription(Unit unit)
        {
            _units.Add(unit);
            SubscribeUnit(unit);
        }

        private void RemoveUnitWithUnsubscription(Unit unit)
        {
            UnsubscribeUnit(unit);
            _units.Remove(unit);
        }

        private void SubscribeUnit(Unit unit)
        {
            unit.Context.OnUnitFree += OnUnitFreeHandle;
            unit.Context.OnUnitBusy += OnUnitBusyHandle;
        }

        private void UnsubscribeUnit(Unit unit)
        {
            unit.Context.OnUnitFree -= OnUnitFreeHandle;
            unit.Context.OnUnitBusy -= OnUnitBusyHandle;
        }

        private void OnUnitFreeHandle(Unit unit)
        {
            OnFreeUnitAvailable?.Invoke();
        }

        private void OnUnitBusyHandle(Unit obj)
        {
            if (_units.All(unit => unit.Context.Status == UnitStatus.Busy))
                OnAllUnitsBusy?.Invoke();
        }

        public bool HasWorkers()
        {
            return _units.Count > 0;
        }
        
        public bool HasFreeWorkers()
        {
            return _units.Any(unit => unit.Context.Status == UnitStatus.Free);
        }

        public Unit UnregisterFirstFreeWorker()
        {
            var unit = FindFirstFreeWorker();
            if (unit == null)
                return null;

            RemoveUnitWithUnsubscription(unit);
            OnWorkerListUpdated?.Invoke();
            return unit;
        }
    }
}