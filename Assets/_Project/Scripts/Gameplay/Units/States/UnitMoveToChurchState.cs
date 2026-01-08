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
            // _unit.Context.SetUnitStatus(UnitStatus.Busy);
            // var churchPosition = _buildingSlotsService.GetChurchPosition();
            // await _unit.Mover.MoveTo(churchPosition);
            // _unit.StateMachine.Enter<UnitAddToChurchQueueState>();
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }
}