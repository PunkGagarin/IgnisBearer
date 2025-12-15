using System;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Level;
using _Project.Scripts.Gameplay.Units;
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

        [Inject]
        private BuildingsService _buildingsService;

        [Inject]
        private BuildingSlotsService _buildingSlotsService;

        [Inject]
        private FateService _fateService;


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
            _buildingSlotsService.InitSlots(_levelService.GetInitialBuildingsSpawnPoints(),
                _levelService.GetChurchBuildingSpawnPoint());
            _buildingsService.InitChurch(_buildingSlotsService.GetChurchSlot());
            var fateStorage = _buildingsService.GetChurch().FateGenerator.GetComponent<IResourceStorage>();
            _fateService.Init(fateStorage);
            _buildingsService.InitChurchGrade();
            _buildingsService.InitHouse(_buildingSlotsService.GetFirstSlot());
            _workerService.CreateStartUnit(_levelService.GetInitalUnitPosition());
        }

        private bool HasProgress()
        {
            return false;
        }

        private void LoadProgress()
        {
            throw new NotImplementedException();
        }

    }

}