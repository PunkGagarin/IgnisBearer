using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.SkillTree.Effectors;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly UnitSettings _unitSettings;
        [Inject] private readonly List<IUnitMoveInfluencer> _moveSpeedInfluencers;

        private readonly List<Type> _initStates = new()
        {
            typeof(UnitMoveToWithIdleAfterState),
            typeof(UnitMoveToWithNext),
            typeof(UnitMoveToWithNextAndPayload),
            typeof(DisableState),
            typeof(UnitIdleState),
            typeof(UnitWaitState),
            typeof(UnitMoveToState),
            typeof(FireUpLanternState),
            typeof(HarvestResourceState),
            typeof(UnitSendLightToChurchState),
        };

        public Unit CreateAndInstantiateUnit(Transform unitPosition)
        {
            var unit = _container.InstantiatePrefabForComponent<Unit>(_unitSettings.UnitPrefab,
                unitPosition.transform.position, Quaternion.identity, unitPosition.transform);

            var moveModifiers = _moveSpeedInfluencers
                .Where(el => el.IsPersist())
                .Select(el => el.GetSpeedModifier())
                .ToList();

            var moveSpeed = new UnitStat(UnitStatType.MoveSpeed, _unitSettings.MoveSpeed, moveModifiers);
            var unitContext = new UnitContext(unit, moveSpeed);

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