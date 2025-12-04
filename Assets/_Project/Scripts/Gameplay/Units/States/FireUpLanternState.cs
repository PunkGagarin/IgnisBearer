using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class FireUpLanternState : IUnitState, IPayloadState<TemporalLantern>
    {
        private float _fireUpDuration = 1f;
        private float _currentTime = 0f;
        
        //todo: bar
        // private LanternBar _bar;
        private TemporalLantern _lantern;

        private Unit _unit;


        public void Init(Unit unit)
        {
            _unit = unit;
        }


        public void Enter(TemporalLantern lantern)
        {
            _lantern = lantern;
        }

        public void Update()
        {
            _currentTime += Time.deltaTime * _unit.Context.FireUpSpeed;

            UpdateBar();
            if (_currentTime > _fireUpDuration)
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
            _unit.Context.Status = UnitStatus.Free;
        }
    }
}