using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Infrastructure.GameStates
{
    public class GameStateMachine : SimpleStateMachine<IGameState>
    {

        // [Inject]
        // public GameStateMachine(List<IGameState> states)
        // {
        //     Debug.Log(" GameStateMachine Inject ");
        //     foreach (var state in states)
        //         Register(state);
        // }

    }
}