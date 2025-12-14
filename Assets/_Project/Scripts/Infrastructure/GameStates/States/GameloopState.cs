using UnityEngine;

namespace _Project.Scripts.Infrastructure.GameStates.States
{
    public class GameloopState : IState, IGameState
    {

        public void Enter()
        {
            Debug.Log(" GameloopState.Enter");
            Object.Instantiate(Resources.Load("Player"));
        }


        public void Exit()
        {
        }
    }
}