using _Project.Scripts.Gameplay.Units.Machine;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitFactory
    {
        [Inject] private readonly DiContainer _container;

        [Inject] private readonly UnitSettings _unitSettings;
        [Inject] private readonly UnitSpawnPoint _unitSpawnPoint;

        public void CreateAndInstantiateUnit()
        {
            var unit = _container.InstantiatePrefabForComponent<PeonUnit>(_unitSettings.PeonUnitPrefab,
                _unitSpawnPoint.transform.position, Quaternion.identity, _unitSpawnPoint.transform);

            var unitContext = new UnitContext(_unitSettings.DefaultMoveSpeed);
            var unitStateMachine = new UnitStateMachine();

            unitStateMachine.Register(new UnitIdleState(unitStateMachine));
            unitStateMachine.Register(new UnitMoveToHouseState(unitStateMachine));

            unit.Construct(unitStateMachine, unitContext);
        }
    }

}