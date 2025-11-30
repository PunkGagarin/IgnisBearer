using _Project.Scripts.Gameplay.Units.Machine;
using _Project.Scripts.Gameplay.Units.Manager;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitFactory
    {
        [Inject] private readonly DiContainer _container;

        [Inject] private readonly UnitSettings _unitSettings;
        [Inject] private readonly UnitSpawnPoint _unitSpawnPoint;

        public Unit CreateAndInstantiateUnit()
        {
            var unit = _container.InstantiatePrefabForComponent<Unit>(_unitSettings.UnitPrefab,
                _unitSpawnPoint.transform.position, Quaternion.identity, _unitSpawnPoint.transform);

            var unitContext = new UnitContext(_unitSettings.DefaultMoveSpeed, _unitSettings.DefaultFireUpSpeed);
            var unitStateMachine = new UnitStateMachine();

            unitStateMachine.Register(new UnitIdleState(unitStateMachine));
            unitStateMachine.Register(new UnitMoveToLanternState(unit));
            unitStateMachine.Register(new FireUpLanternState(unit));
            unitStateMachine.Register(new HarvestLanternState(unit));

            unit.Construct(unitStateMachine, unitContext);
            
            return unit;
        }
    }

}