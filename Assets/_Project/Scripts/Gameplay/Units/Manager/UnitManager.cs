using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Temporal;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units.Manager
{
    public class UnitManager : MonoBehaviour
    {
        [Inject] private UnitFactory _factory;

        [field: SerializeField]
        private TempClickDetector _clickDetector;

        private List<Unit> _units = new();

        private void Awake()
        {
            _clickDetector.OnClicked += MoveFreeUnit;
        }

        private void Start()
        {
            CreateAndRegisterUnit();
        }

        public void CreateAndRegisterUnit()
        {
            var unit = _factory.CreateAndInstantiateUnit();
            RegisterUnit(unit);
        }

        private void MoveFreeUnit(TemporalLantern lantern)
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