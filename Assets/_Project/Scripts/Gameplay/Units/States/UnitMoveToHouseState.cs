using _Project.Scripts.Gameplay.Units.Machine;
using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMoveToHouseState : IState, IUnitState
    {
        private PeonUnit _peonUnit;
        
        private UnitStateMachine _unitStateMachine;

        public UnitMoveToHouseState(UnitStateMachine unitStateMachine)
        {
        }

        public void Enter()
        {
            // await _peonUnit.Mover.MoveTo(_peonUnit._house.GetPosition());
            // // _unit.DoThis();
        }

        public void Exit()
        {
        }

        public void Update()
        {

        }
    }
}