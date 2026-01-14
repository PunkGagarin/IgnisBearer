using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;
using _Project.Scripts.Gameplay.Data;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingsService
    {
        [Inject] private readonly ChurchFactory _churchFactory;
        [Inject] private readonly HouseFactory _houseFactory;
        [Inject] private readonly FateGeneratorFactory _fateGeneratorFactory;
        [Inject] private readonly AutoHarvesterFactory _autoHarvesterFactory;
        [Inject] private readonly AutoLighterFactory _autoLighterFactory;
        [Inject] private readonly FactoryBuildingFactory _factoryBuildingFactory;
        [Inject] private readonly PlayerDataService _playerDataService;
        [Inject] private readonly BuildingSlotsService _slotsService;

        private List<Building> _buildings = new();
        private ChurchBuilding _church;
        private int initGrade = 1; //todo from save

        public event Action<FateGeneratorBuilding> OnFateGeneratorBuilt = delegate { };

        //todo: madgine а почему не решается через GameplayBootstraper?
        public event Action<ChurchBuilding> OnChurchBuilt = delegate { };

        public void InitChurch(BuildingSlot slot)
        {
            AddBuildingTo(BuildingType.Church, slot);
        }

        public void InitHouse(BuildingSlot slot)
        {
            AddBuildingTo(BuildingType.House, slot);
        }

        public void AddBuildingTo(BuildingType buildingType, BuildingSlot buildingSlot)
        {
            Building building = buildingType switch
            {
                BuildingType.Church => _churchFactory.Create(buildingSlot, initGrade),
                BuildingType.House => _houseFactory.Create(buildingSlot, initGrade),
                BuildingType.Factory => _factoryBuildingFactory.Create(buildingSlot, initGrade),
                BuildingType.AutoHarvest => _autoHarvesterFactory.Create(buildingSlot, initGrade),
                BuildingType.AutoLighter => _autoLighterFactory.Create(buildingSlot, initGrade),
                BuildingType.FateGenerator => _fateGeneratorFactory.Create(buildingSlot, initGrade),
                _ => throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null)
            };
            RegisterBuilding(building);
        }

        private void RegisterBuilding(Building building)
        {
            if (building is ChurchBuilding churchBuilding)
            {
                _church = churchBuilding;
                OnChurchBuilt.Invoke(churchBuilding);
            }
            else if (building is FateGeneratorBuilding fateGenerator)
            {
                _buildings.Add(building);
                OnFateGeneratorBuilt.Invoke(fateGenerator);
            }
            else
                _buildings.Add(building);
        }

        public T GetBuilding<T>() where T : Building
        {
            return _buildings.OfType<T>().FirstOrDefault();
        }

        public int GetBuildingCountByType(BuildingType buildingType) =>
            _buildings.Count(x => x.Type == buildingType);

        public ChurchBuilding GetChurch() => _church;

        public void InitPrebuildFor(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.Church:
                    InitChurch(_slotsService.GetChurchSlot());
                    break;
                default:
                    AddBuildingTo(buildingType, _slotsService.GetFirstSlot());
                    return;
            }
        }
    }
}