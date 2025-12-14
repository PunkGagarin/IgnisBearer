using _Project.Scripts.Gameplay.Buildings;
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
        
        //todo: bar
        // private LanternBar _bar;
        private Lantern _lantern;

        private Unit _unit;


        public void Init(Unit unit)
        {
            _unit = unit;
        }


        public void Enter(Lantern lantern)
        {
            _lantern = lantern;
        }

        public void Update()
        {
            _currentTime += Time.deltaTime * _unit.Context.FireUpMultiplier;

            UpdateBar();
            if (_currentTime > _lanternSettings.FireUpTime)
            {
                _lantern.FireUp();
                _unit.StateMachine.Enter<UnitIdleState>();
            }
        }

        private void UpdateBar()
        {
            //todo: implement me
        }

        public void Exit()
        {
            _currentTime = 0f;
            _lantern = null;
        }
    }
}