using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units
{

    public class DisableState : IUnitState, IState
    {
        private Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Update()
        {
        }

        public void Enter()
        {
            _unit.gameObject.SetActive(false);
        }

        public void Exit()
        {
        }
    }

}