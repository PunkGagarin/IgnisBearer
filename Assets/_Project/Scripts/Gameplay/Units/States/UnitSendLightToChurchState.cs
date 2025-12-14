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

        private ResourceStorage ChurchResourceStorage => _buildingsService.GetChurch().GetComponent<ResourceStorage>();
        private IGrade ChurchGrade => _buildingsService.GetChurch().GetComponent<IGrade>();

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
            _currentTime += Time.deltaTime * _unit.Context.FireUpMultiplier;

            UpdateBar();

            if (_currentTime > _churchSettings.GradeData[ChurchGrade.Current].LightSendSpeed)
            {
                ChurchResourceStorage.IncrementAmount(_unit.Context.LightAmount);
                _unit.Context.LightAmount = 0;
                _unit.StateMachine.Enter<UnitIdleState>();
            }
        }

        public void Exit()
        {
            _currentTime = 0f;
        }

        private void UpdateBar()
        {
        }
    }
}