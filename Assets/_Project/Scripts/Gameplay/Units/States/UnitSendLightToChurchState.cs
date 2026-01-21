using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Ui.UiEffects;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitSendLightToChurchState : IUnitState, IPayloadState<ChurchLightSendSlot>
    {
        [Inject] private BuildingsService _buildingsService;
        [Inject] private UnitSettings _unitSettings;
        [Inject] private ChurchSettings _churchSettings;

        private ResourceStorage ChurchResourceStorage => _buildingsService.GetChurch().GetComponent<ResourceStorage>();

        private Unit _unit;

        private float _currentTime = 0f;
        private ChurchLightSendSlot _slot;
        private float _lightSendSpeed;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Enter(ChurchLightSendSlot slot)
        {
            _slot = slot;
            
            //todo: stat move from settings or add more complex logic
            _lightSendSpeed = _buildingsService.GetChurch().GetLightSendSpeed();
            
            //todo: move to church queue and incapsualte???
            slot.SetBusy();
            
            var uiPulseShot = slot.GetComponent<UiPulseShot>();
            uiPulseShot.PlayPulse();
            
            _unit.SetVisualStatus(false);
        }

        public void Update()
        {
            _currentTime += Time.deltaTime * _unit.Context.FireUpMultiplier;

            UpdateBar();

            if (_currentTime > _lightSendSpeed)
            {
                var amountPerLight = _buildingsService.GetChurch().GetAmountPerLight();
                var contextLightAmount = _unit.Context.LightAmount * amountPerLight;
                ChurchResourceStorage.IncrementAmount(contextLightAmount);
                _slot.SetFree();
                _unit.SetVisualStatus(true);
                _unit.StateMachine.Enter<UnitIdleState>();
            }
        }

        public void Exit()
        {
            _currentTime = 0f;
            _unit.Context.LightAmount = 0;
        }

        private void UpdateBar()
        {
            _slot.Progress(_currentTime / _lightSendSpeed);
        }
    }
}