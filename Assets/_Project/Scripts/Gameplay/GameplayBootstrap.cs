using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units.Manager;
using _Project.Scripts.Infrastructure.GameStates.States;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class GameplayBootstrap : IInitializable
    {
        [Inject]
        private LanternService _lanternService;

        [Inject]
        private WorkerService _workerService;

        [Inject]
        private LevelFactory _levelFactory;


        public void Initialize()
        {
            if (HasProgress())
                LoadProgress();
            else
                InitLevel();
        }

        private void InitLevel()
        {
            var level = _levelFactory.CreateLevel();
            _lanternService.InitStartLanterns(level.InitalLanternPositions);
            _workerService.CreateStartUnit(level.InitalUnitPosition);
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