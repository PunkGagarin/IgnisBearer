using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitSendLightToChurchState : IUnitState, IState
    {
        [Inject] private BuildingsService _buildingsService;
        [Inject] private UnitSettings _unitSettings;
        [Inject] private ChurchSettings _churchSettings;

        private LightStorage ChurchLightStorage => _buildingsService.GetChurch().GetComponent<LightStorage>();

        private Unit _unit;

        private float _currentTime = 0f;

        // private Action _enterNextState;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Enter()
        {
        }

        public void Update()
        {
            _currentTime += Time.deltaTime;

            UpdateBar();

            if (_currentTime > _churchSettings.LightSendSpeed)
            {
                ChurchLightStorage.IncrementAmount(_unit.Context.LightAmount);
                _unit.Context.LightAmount = 0;
                _unit.StateMachine.Enter<UnitIdleState>();
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