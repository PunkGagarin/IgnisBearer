using _Project.Scripts.Infrastructure.GameStates;

namespace _Project.Scripts.Gameplay.Units
{
    public interface IEnterWithPayloadAndNextPayload<TPayload> : IExitableState
    {
        void Enter<TNextState, TNextPayload>(TPayload payload, TNextPayload nextPayload) where TNextState : class, IPayloadState<TNextPayload>, IUnitState;
    }
}