using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.Lanterns;
using _Project.Scripts.Gameplay.Data;
using _Project.Scripts.Gameplay.Level;
using _Project.Scripts.Gameplay.SkillTree;
using _Project.Scripts.Gameplay.Tutorial;
using _Project.Scripts.Gameplay.Ui;
using _Project.Scripts.Gameplay.Units;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class GameplayBootstrap : IInitializable, IDisposable
    {
        [Inject] private LanternService _lanternService;
        [Inject] private LanternSlotsService _lanternSlotsService;
        [Inject] private WorkerService _workerService;
        [Inject] private LevelService _levelService;
        [Inject] private BuildingsService _buildingsService;
        [Inject] private BuildingSlotsService _buildingSlotsService;
        [Inject] private FateService _fateService;
        [Inject] private GameEndService _gameEndService;
        [Inject] private SkillTreeService _skillTreeService;
        [Inject] private PlayerDataService _playerDataService;
        [Inject] private MetaCurrencyService _metaCurrencyService;
        [Inject] private BuildingSlotEnabler _slotEnabler;
        [Inject] private TutorialService _tutorial;
        [Inject] private StartDataFactory _dataFactory;

        public async void Initialize()
        {
            if (HasProgress())
                await _playerDataService.Load();
            else
                _dataFactory.CreateStartData();

            CreateGame();
        }

        private void CreateGame()
        {
            _levelService.CreateLevel();
            _metaCurrencyService.Init(_playerDataService.PlayerData.CurrencyData.Currencies);
            CreateBuildingSlots();
            CreatePrebuildBuildings();
            InitConsumeProgressor();
            _slotEnabler.Init();
            InitLanternSlots();
            InitLanterns();

            _skillTreeService.Init(_playerDataService.PlayerData.SkillTreeData);
            _gameEndService.Init();
            _tutorial.StartTutor();
        }

        private void InitConsumeProgressor()
        {
            _buildingsService
                .GetChurch()
                .GetComponent<LightConsumeProgressor>()
                .Init();
        }

        private void CreateBuildingSlots()
        {
            var slotCount = _playerDataService.PlayerData.BuildingData.StartBuildingSlotCount;
            _buildingSlotsService.InitSlots(_levelService.GetInitialBuildingsSpawnPoints(),
                _levelService.GetChurchBuildingSpawnPoint(), slotCount);
        }

        private void InitLanternSlots()
        {
            var slotsCount = _playerDataService.PlayerData.BuildingData.StartLanternSlotCount;
            _lanternSlotsService.InitSlots(
                _levelService.GetInitialLanternSlotsPositions(),
                _levelService.GetAdditionalLanternSlotsPositions(),
                slotsCount
            );
        }

        private void CreatePrebuildBuildings()
        {
            List<BuildingType> prebuildBuildings = _playerDataService.PlayerData.BuildingData.PrebuildBuildings;
            foreach (var buildingType in prebuildBuildings)
                _buildingsService.InitPrebuildFor(buildingType);
        }

        private void InitLanterns()
        {
            //todo: количетво стартовыъ фонарей может зависить от сохранки? Узнать у Вити идобавить если надо
            _lanternService.InitStartLanterns(_lanternSlotsService.GetInitialSlots());
        }

        private bool HasProgress()
        {
            return _playerDataService.HasProgress();
        }

        public void Dispose()
        {
            _playerDataService.Save();
        }
    }
}