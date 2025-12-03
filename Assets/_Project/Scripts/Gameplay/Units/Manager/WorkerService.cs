using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units.Manager
{
    public class WorkerService : IInitializable
    {
        [Inject] private UnitFactory _factory;

        private List<Unit> _units = new();
        
        public void Initialize()
        {
            CreateStartUnit();
        }

        public void CreateStartUnit()
        {
            Debug.Log(" CreateStartUnit");
            CreateAndRegisterUnit();
        }

        public void CreateAndRegisterUnit()
        {
            var unit = _factory.CreateAndInstantiateUnit();
            RegisterUnit(unit);
        }

        //todo: move out to another class
        public void MoveFreeUnit(TemporalLantern lantern)
        {
            var unit = FindFirstFreeWorker();
            unit.Context.MoveTarget = lantern.GetPosition();
            unit.StateMachine.Enter<UnitMoveToLanternState, TemporalLantern>(lantern);
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