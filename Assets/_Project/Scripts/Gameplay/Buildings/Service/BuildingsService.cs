using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.AutoCollector;
using _Project.Scripts.Gameplay.Buildings.AutoLighter;
using _Project.Scripts.Gameplay.Buildings.Factory;
using _Project.Scripts.Gameplay.BuildingsSlots;
using _Project.Scripts.Localization;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Service
{
    public class BuildingsService
    {
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoCollectorSettings _autoCollectorSettings;
        [Inject] private AutoLighterSettings _autoLighterSettings;
        [Inject] private BuildingFactory _buildingFactory;
        [Inject] private LocalizationTool _localizationTool;


        // how to init church & house
        public void InitSlots(List<BuildingSlotsSpawnPoint> buildingsSpawnPoints)
        {
            foreach (var buildingSlotsSpawnPoint in buildingsSpawnPoints)
            {
                _buildingFactory.CreateSlotAtPosition(buildingSlotsSpawnPoint);
            }
        }

        public void AddBuildingTo(BuildingType buildingType, BuildingSlot buildingSlot)
        {
            _buildingFactory.BuildByType(buildingType, buildingSlot);
        }

        public List<BuildingButtonData> GetAddBuildingPopupData()
        {
            List<BuildingButtonData> list = new List<BuildingButtonData>();
            AddFactoryButton(list);
            AddAutoCollectorButton(list);
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

        private void AddAutoCollectorButton(List<BuildingButtonData> list)
        {
            var autoCollectorInitPrice = GetAutoCollectorInitPrice();
            if (CanBuildAutoCollector(autoCollectorInitPrice))
            {
                list.Add(new BuildingButtonData(BuildingType.AutoCollector, autoCollectorInitPrice,
                    Localize(_autoCollectorSettings.BuildingNameKey)));
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

        private bool CanBuildAutoCollector(double price) => HaveEnoughMoney(price);

        private bool CanBuildAutoLighter(double price) => HaveEnoughMoney(price);

        private double GetFactoryInitPrice() => _factorySettings.BuildPrice;

        private double GetAutoCollectorInitPrice() => _autoCollectorSettings.BuildPrice;

        private double GetAutoLighterInitPrice() => _autoLighterSettings.BuildPrice;

        private string Localize(string key) => _localizationTool.GetText(key);


        private bool HaveEnoughMoney(double price)
        {
            return true; // todo gold service?
        }
    }
}