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
        public void MoveFreeUnit(Lantern lantern)
        {
            var unit = FindFirstFreeWorker();
            unit?.StateMachine.Enter<UnitMoveToLanternState, Lantern>(lantern);
        }

        private Unit FindFirstFreeWorker()
        {
            if (_units.Count <= 0)
            {
                Debug.LogWarning("Trying to find free worker when there are no workers");
                return null;
            }
            return _units.FirstOrDefault();
        }

        public void RegisterUnit(Unit unit)
        {
            _units.Add(unit);
            OnWorkerListUpdated?.Invoke();
        }

        public bool HasWorkers()
        {
            return _units.Count > 0;
        }

        public Unit UnregisterFirstFreeWorker()
        {
            var unit = FindFirstFreeWorker();
            _units.Remove(unit);
            OnWorkerListUpdated?.Invoke();
            return unit;
        }
    }
}