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
        [Inject] private LanternService _lanternService;
        [Inject] private LanternSlotsService _lanternSlotsService;

        [Inject] private WorkerService _workerService;

        [Inject] private LevelService _levelService;

        [Inject] private BuildingsService _buildingsService;

        [Inject] private BuildingSlotsService _buildingSlotsService;

        [Inject] private FateService _fateService;

        [Inject] private GameEndService _gameEndService;


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
            _workerService.CreateStartUnit(_levelService.GetInitalUnitPosition());

            InitBuildingSlots();
            InitExistingChurch();

            InitFateService();
            _buildingsService.InitChurchGrade();
            InitConsumeProgressor();

            InitExistingHouse();

            InitLanternSlots();
            InitLanterns();

            _gameEndService.Init();
        }

        private void InitExistingHouse()
        {
            _buildingsService.InitHouse(_buildingSlotsService.GetFirstSlot());
        }

        private void InitConsumeProgressor()
        {
            _buildingsService
                .GetChurch()
                .GetComponent<LightConsumeProgressor>()
                .Init();
        }

        private void InitLanternSlots()
        {
            _lanternSlotsService.InitSlots(
                _levelService.GetInitialLanternSlotsPositions(),
                _levelService.GetAdditionalLanternSlotsPositions()
            );
        }

        private void InitBuildingSlots()
        {
            _buildingSlotsService.InitSlots(_levelService.GetInitialBuildingsSpawnPoints(),
                _levelService.GetChurchBuildingSpawnPoint());
        }

        private void InitExistingChurch()
        {
            _buildingsService.InitChurch(_buildingSlotsService.GetChurchSlot());
        }

        private void InitFateService()
        {
            var fateStorage = _buildingsService.GetChurch().FateGenerator.GetComponent<IResourceStorage>();
            _fateService.Init(fateStorage);
        }

        private void InitLanterns()
        {
            _lanternService.InitStartLanterns(_lanternSlotsService.GetInitialSlots());
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