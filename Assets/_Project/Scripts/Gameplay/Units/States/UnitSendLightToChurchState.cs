using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
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
        
        private ChurchBuilding Church => _buildingsService.GetChurch();

        private Unit _unit;

        private float _currentTime = 0f;
        private LightStorage _lightStorage;

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
                var resource = _lightStorage.Harvest();
                //todo: complete logic
            
                // _enterNextState?.Invoke();
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