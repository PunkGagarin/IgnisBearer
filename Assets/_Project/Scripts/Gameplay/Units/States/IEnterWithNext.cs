using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units
{
    public interface IEnterWithNext<in TPayload> : IExitableState
    {
        void Enter<TNextState>(TPayload payload) where TNextState : class, IState, IUnitState;
    }
}