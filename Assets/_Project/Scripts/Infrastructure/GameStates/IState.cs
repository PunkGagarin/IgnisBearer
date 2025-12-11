using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure.GameStates
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IExitableState
    {
        void Exit();
    }

    public interface IPayloadState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
    
    public interface IPayloadState<TPayload, TPayload2> : IExitableState
    {
        void Enter(TPayload payload, TPayload2 payload2);
    }

    public interface IUpdateState
    {
        void Update();
    }

    public interface IGameState : IExitableState
    {
    }
}