using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Localization;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingsService
    {
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoHarvestSettings _autoHarvestSettings;
        [Inject] private AutoLighterSettings _autoLighterSettings;
        [Inject] private BuildingFactory _buildingFactory;
        [Inject] private LocalizationTool _localizationTool;

        private List<BuildingSlot> _buildingSlots = new();
        private BuildingSlot _churchSlot;

        private List<Building> _buildings = new();
        private ChurchBuilding _church;

        public void InitSlots(List<BuildingSlotsSpawnPoint> buildingsSpawnPoints,
            BuildingSlotsSpawnPoint churchBuildingSpawnPoint)
        {
            InitChurchSlot(churchBuildingSpawnPoint);
            foreach (var buildingSlotsSpawnPoint in buildingsSpawnPoints)
            {
                var slot = _buildingFactory.CreateSlotAtPosition(buildingSlotsSpawnPoint);
                _buildingSlots.Add(slot);
            }
        }

        private void InitChurchSlot(BuildingSlotsSpawnPoint churchBuildingSpawnPoint)
        {
            var church = _buildingFactory.CreateSlotAtPosition(churchBuildingSpawnPoint);
            _churchSlot = church;
        }

        public void InitBuildings()
        {
            AddBuildingTo(BuildingType.Church, _churchSlot);
            AddBuildingTo(BuildingType.House, _buildingSlots.First());
        }

        public Building AddBuildingTo(BuildingType buildingType, BuildingSlot buildingSlot)
        {
            var building = _buildingFactory.BuildByType(buildingType, buildingSlot);
            RegisterBuilding(building);
            return building;
        }

        private void RegisterBuilding(Building building)
        {
            if (building is ChurchBuilding churchBuilding)
                _church = churchBuilding;
            else
                _buildings.Add(building);
        }

        public ChurchBuilding GetChurch() => _church;
        
        public List<BuildingButtonData> GetAddBuildingPopupData()
        {
            List<BuildingButtonData> list = new List<BuildingButtonData>();
            AddFactoryButton(list);
            AddAutoHarvesterButton(list);
            AddAutoLighterButton(list);
            return list;
        }

        private void AddFactoryButton(List<BuildingButtonData> list)
        {
            var factoryInitPrice = GetFactoryInitPrice();
            if (CanBuildFactory(factoryInitPrice))
            {
                list.Add(new BuildingButtonData(BuildingType.Factory, factoryInitPrice,
                    Localize(_factorySettings.BuildingNameKey)));
            }
        }

        private void AddAutoHarvesterButton(List<BuildingButtonData> list)
        {
            var autoHarvesterInitPrice = GetAutoHarvesterInitPrice();
            if (CanBuildAutoHarvester(autoHarvesterInitPrice))
            {
                list.Add(new BuildingButtonData(BuildingType.AutoHarvest, autoHarvesterInitPrice,
                    Localize(_autoHarvestSettings.BuildingNameKey)));
            }
        }

        private void AddAutoLighterButton(List<BuildingButtonData> list)
        {
            var autoLighterInitPrice = GetAutoLighterInitPrice();
            if (CanBuildAutoLighter(autoLighterInitPrice))
            {
                list.Add(new BuildingButtonData(BuildingType.AutoLighter, autoLighterInitPrice,
                    Localize(_autoLighterSettings.BuildingNameKey)));
            }
        }

        private bool CanBuildFactory(double price) => HaveEnoughMoney(price);

        private bool CanBuildAutoHarvester(double price) => HaveEnoughMoney(price);

        private bool CanBuildAutoLighter(double price) => HaveEnoughMoney(price);

        private double GetFactoryInitPrice() => _factorySettings.BuildPrice;

        private double GetAutoHarvesterInitPrice() => _autoHarvestSettings.BuildPrice;

        private double GetAutoLighterInitPrice() => _autoLighterSettings.BuildPrice;

        private string Localize(string key) => _localizationTool.GetText(key);


        private bool HaveEnoughMoney(double price)
        {
            return true; // todo gold service?
        }

        public Vector3 GetChurchPosition()
        {
            return _churchSlot.transform.position;
        }
    }
}