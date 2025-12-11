using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Infrastructure.GameStates;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToChurchState : IUnitState, IState
    {
        [Inject] BuildingSlotsService _buildingSlotsService;
        private Unit _unit;


        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public async void Enter()
        {
            var churchPosition = _buildingSlotsService.GetChurchPosition();
            
            _unit.Context.Status = UnitStatus.Busy;
            await _unit.Mover.MoveTo(churchPosition);
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