using System;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class HarvestLanternState : IUnitState, IPayloadState<Lantern>
    {
        [Inject] private LanternSettings _lanternSettings;

        private Unit _unit;
        private float _currentTime = 0f;
        private LightStorage _lightStorage;
        private UnitContext Context => _unit.Context;

        // private Action _enterNextState;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Enter(Lantern lantern)
        {
            _lightStorage = lantern.GetComponent<LightStorage>();
            // _enterNextState = () => _unit.StateMachine.Enter<UnitIdleState>();
        }

        public void Update()
        {
            _currentTime += Time.deltaTime * _unit.Context.FireUpSpeed;

            UpdateBar();
            if (_currentTime > _lanternSettings.HarvestTime)
            {
                Context.LightAmount = _lightStorage.Harvest();
                _unit.StateMachine.Enter<UnitMoveToChurchState>();
            }
        }

        public void Exit()
        {
            _unit.Context.Status = UnitStatus.Free;
            _currentTime = 0f;
        }

        private void UpdateBar()
        {
        }
    }
}