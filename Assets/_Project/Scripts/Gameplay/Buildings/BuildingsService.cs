using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.Church;
using _Project.Scripts.Gameplay.Buildings.Factory;
using _Project.Scripts.Gameplay.Buildings.House;
using _Project.Scripts.Gameplay.BuildingsSlots;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingsService
    {
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoCollectorSettings _autoCollectorSettings;
        [Inject] private AutoLighterSettings _autoLighterSettings;
        [Inject] private BuildingFactory _buildingFactory;

        // how to init church & house
        
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
                list.Add(new BuildingButtonData(BuildingType.Factory, factoryInitPrice));
            }
        }

        private void AddAutoCollectorButton(List<BuildingButtonData> list)
        {
            var autoCollectorInitPrice = GetAutoCollectorInitPrice();
            if (CanBuildAutoCollector(autoCollectorInitPrice))
            {
                list.Add(new BuildingButtonData(BuildingType.AutoCollector, autoCollectorInitPrice));
            }
        }

        private void AddAutoLighterButton(List<BuildingButtonData> list)
        {
            var autoLighterInitPrice = GetAutoLighterInitPrice();
            if (CanBuildAutoLighter(autoLighterInitPrice))
            {
                list.Add(new BuildingButtonData(BuildingType.AutoLighter, autoLighterInitPrice));
            }
        }

        private bool CanBuildFactory(double price) => HaveEnoughMoney(price);
        private bool CanBuildAutoCollector(double price) => HaveEnoughMoney(price);
        private bool CanBuildAutoLighter(double price) => HaveEnoughMoney(price);

        private double GetFactoryInitPrice() => _factorySettings.BuildPrice;
        private double GetAutoCollectorInitPrice() => _autoCollectorSettings.BuildPrice;
        private double GetAutoLighterInitPrice() => _autoLighterSettings.BuildPrice;


        private bool HaveEnoughMoney(double price)
        {
            return true; // todo gold service?
        }
    }
}