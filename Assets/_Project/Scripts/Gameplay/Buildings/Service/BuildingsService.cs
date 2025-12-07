using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingsService
    {
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoHarvestSettings _autoHarvestSettings;
        [Inject] private AutoLighterSettings _autoLighterSettings;
        [Inject] private HouseSettings _houseSettings;
        [Inject] private BuildingFactory _buildingFactory;

        private List<Building> _buildings = new();
        private ChurchBuilding _church;

        

        public void InitInitialBuildings(BuildingSlot churchSlot, BuildingSlot houseSlot)
        {
            AddBuildingTo(BuildingType.Church, churchSlot);
            AddBuildingTo(BuildingType.House, houseSlot);
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
            else
                _buildings.Add(building);
        }

        public T GetBuilding<T>() where T : Building
        {
            return _buildings.OfType<T>().FirstOrDefault();
        }
        
        public ChurchBuilding GetChurch() => _church;

        public float GetUnitPurchaseData() => _houseSettings.UnitPrice;
    }
}