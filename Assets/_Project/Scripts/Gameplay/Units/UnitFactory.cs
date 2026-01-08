using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly UnitSettings _unitSettings;

        private readonly List<Type> _initStates = new()
        {
            typeof(SimpleMoveToState),
            typeof(UnitMoveToWithNext),
            typeof(MoveToWithNextAndPayload),

            typeof(DisableState),
            typeof(UnitIdleState),
            typeof(UnitWaitState),
            typeof(FireUpLanternState),
            typeof(HarvestResourceState),
            typeof(UnitAddToChurchQueueState),
            typeof(UnitSendLightToChurchState),
        };

        public Unit CreateAndInstantiateUnit(Transform unitPosition)
        {
            var unit = _container.InstantiatePrefabForComponent<Unit>(_unitSettings.UnitPrefab,
                unitPosition.transform.position, Quaternion.identity, unitPosition.transform);

            var unitContext = new UnitContext(unit,
                _unitSettings.MoveSpeed,
                _unitSettings.FireUpMultiplier,
                _unitSettings.SendLightToChurchMultiplier
            );
            var unitStateMachine = new UnitStateMachine();

            foreach (var stateType in _initStates)
                CreateState(stateType, unit, unitStateMachine);

            unit.Construct(unitStateMachine, unitContext);

            return unit;
        }

        private void CreateState(Type type, Unit unit, UnitStateMachine unitStateMachine)
        {
            var idle = _container.Instantiate(type) as IUnitState;
            idle?.Init(unit);
            unitStateMachine.Register(idle);
        }
    }
}