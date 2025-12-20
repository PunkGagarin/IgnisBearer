using System;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Infrastructure.GameStates;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class HarvestResourceState : IUnitState, IPayloadState<LightResource>
    {
        // [Inject] private LanternSettings _lanternSettings;

        [Inject] BuildingSlotsService _buildingSlotsService;
        private Unit _unit;

        // private float _currentTime = 0f;
        private UnitContext Context => _unit.Context;


        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter(LightResource resource)
        {
            resource.Harvest();

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            Context.LightAmount += 1;
            _unit.StateMachine.Enter<UnitMoveToWithNext, UnitSendLightToChurchState, Vector3>(
                _buildingSlotsService.GetChurchPosition());
        }

        public void Update()
        {
            // _currentTime += Time.deltaTime * _unit.Context.FireUpMultiplier;
            //
            // UpdateBar(_currentTime, _lanternSettings.HarvestTime);
            //
            // if (_currentTime > _lanternSettings.HarvestTime)
            // {
            //     Context.LightAmount = _resourceStorage.Collect();
            //     _unit.StateMachine.Enter<UnitMoveToChurchState>();
            // }
        }

        public void Exit()
        {
            // _currentTime = 0f;
            // _resourceStorage = null;
            // _lanternUi = null;
        }

        // private void UpdateBar(float currentTime, float lanternSettingsHarvestTime)
        // {
        //     _lanternUi.SetProgress(currentTime / lanternSettingsHarvestTime);
        // }
    }
}