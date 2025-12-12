using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units
{
    public interface IUnitState : IExitableState, IUpdateState, IUnitInitState
    {
    }

    public interface IUnitInitState
    {
        public void Init(Unit unit);
    }
}