using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class FireUpLanternState : IUnitState, IPayloadState<Lantern>
    {
        [Inject] private LanternSettings _lanternSettings;
        
        private float _currentTime = 0f;
        
        private LanternUi _lanternUi;
        private Lantern _lantern;

        private Unit _unit;


        public void Init(Unit unit)
        {
            _unit = unit;
        }
        
        public void Enter(Lantern lantern)
        {
            _lantern = lantern;
            _lanternUi = lantern.GetComponent<LanternUi>();
        }

        public void Update()
        {
            _currentTime += Time.deltaTime;

            UpdateBar();
            if (_currentTime > _lanternSettings.FireUpTime)
            {
                _lantern.FireUp();
                _unit.StateMachine.Enter<UnitIdleState>();
            }
        }

        private void UpdateBar()
        {
            var progress = Mathf.Clamp01(_currentTime / _lanternSettings.FireUpTime);
            _lanternUi.SetProgress(progress);
        }

        public void Exit()
        {
            _currentTime = 0f;
            _lantern = null;
        }
    }
}