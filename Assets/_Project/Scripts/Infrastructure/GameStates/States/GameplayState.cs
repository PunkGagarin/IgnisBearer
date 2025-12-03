using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units.Manager;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.GameStates.States
{
    public class GameplayState : IState, IGameState
    {
        //todo: fix later with moving into project context
        // [Inject]
        // private LanternService _lanternService;
        //
        // [Inject]
        // private WorkerService _workerService;

        public void Enter()
        {
            // if (HasProgress())
            //     LoadProgress();
            // else
            // {
            //     _lanternService.InitStartLanterns();
            //     _workerService.CreateStartUnit();
            // }
        }


        public void Exit()
        {
        }

        private bool HasProgress()
        {
            return false;
        }

        private void LoadProgress()
        {
            throw new System.NotImplementedException();
        }
    }
}