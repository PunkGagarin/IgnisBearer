using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingsService
    {
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoHarvestSettings _autoHarvestSettings;
        [Inject] private AutoLighterSettings _autoLighterSettings;
        [Inject] private HouseSettings _houseSettings;
        [Inject] private ChurchSettings _churchSettings;
        [Inject] private BuildingFactory _buildingFactory;
        [Inject] private BuildingComponentsInitService _buildingComponentsInitService;

        private List<Building> _buildings = new();
        private ChurchBuilding _church;
        
        public event Action<FateGeneratorBuilding> OnFateGeneratorBuilt = delegate { };

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
            var building = _buildingFactory.BuildByType(buildingType, buildingSlot);
            RegisterBuilding(building);
        }

        private void RegisterBuilding(Building building)
        {
            if (building is ChurchBuilding churchBuilding)
                _church = churchBuilding;
            else if (building is FateGeneratorBuilding fateGenerator)
            {
                OnFateGeneratorBuilt.Invoke(fateGenerator);
            }
            else
                _buildings.Add(building);
        }

        public T GetBuilding<T>() where T : Building
        {
            return _buildings.OfType<T>().FirstOrDefault();
        }

        public ChurchBuilding GetChurch() => _church;
    }
}