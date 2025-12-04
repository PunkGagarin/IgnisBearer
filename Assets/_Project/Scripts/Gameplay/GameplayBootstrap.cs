using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Units.Manager;
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
        private LevelService _levelService;


        public void Initialize()
        {
            if (HasProgress())
                LoadProgress();
            else
                InitLevel();
        }

        private void InitLevel()
        {
            _levelService.CreateLevel();
            _lanternService.InitStartLanterns(_levelService.GetInitialLanternPositions());
            _workerService.CreateStartUnit(_levelService.GetInitalUnitPosition());
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