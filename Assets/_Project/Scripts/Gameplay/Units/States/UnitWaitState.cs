using _Project.Scripts.Gameplay.Level;
using _Project.Scripts.Infrastructure.GameStates;
using Zenject;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitWaitState : IState, IUnitState
    {
        [Inject] private UnitSettings _unitSettings;
        [Inject] private LevelService _levelService;

        private Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public void Enter()
        {
            //todo: anim start animation
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}