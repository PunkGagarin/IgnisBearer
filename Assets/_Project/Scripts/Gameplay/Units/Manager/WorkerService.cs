using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units.Manager
{
    public class WorkerService
    {
        [Inject] private UnitFactory _factory;

        private List<Unit> _units = new();

        public void CreateStartUnit(UnitSpawnPoint unitPosition)
        {
            Debug.Log(" CreateStartUnit");
            CreateAndRegisterUnit(unitPosition);
        }

        public void CreateAndRegisterUnit(UnitSpawnPoint unitPosition)
        {
            var unit = _factory.CreateAndInstantiateUnit(unitPosition);
            RegisterUnit(unit);
        }

        //todo: move out to another class
        public void MoveFreeUnit(Lantern lantern)
        {
            var unit = FindFirstFreeWorker();
            unit.Context.MoveTarget = lantern.GetPosition();
            unit.StateMachine.Enter<UnitMoveToLanternState, Lantern>(lantern);
        }

        private Unit FindFirstFreeWorker()
        {
            if (_units.Count <= 0)
            {
                Debug.LogError("Trying to find free worker when there are no workers");
                return null;
            }
            return _units.FirstOrDefault();
        }

        private void RegisterUnit(Unit unit)
        {
            _units.Add(unit);
        }

    }
}