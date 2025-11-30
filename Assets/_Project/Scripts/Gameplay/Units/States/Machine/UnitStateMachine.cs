using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units.Machine
{
    public class UnitStateMachine : SimpleStateMachine<IUnitState>
    {

        public void Update()
        {
            _currentState.Update();
        }

    }
}