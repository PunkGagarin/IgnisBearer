using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Infrastructure.GameStates;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToChurchState : IUnitState, IState
    {
        [Inject] BuildingsService _buildingsService;
        private Unit _unit;


        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter()
        {
            _unit.Context.Status = UnitStatus.Busy;

            await _unit.Mover.MoveTo(_buildingsService.GetChurchPosition());
            _unit.StateMachine.Enter<UnitSendLightToChurchState>();
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }

}