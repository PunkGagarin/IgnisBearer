using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Localization;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingAddingOptionsService
    {
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoHarvestSettings _autoHarvestSettings;
        [Inject] private AutoLighterSettings _autoLighterSettings;
        [Inject] private LocalizationTool _localizationTool;

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
            if (HaveEnoughMoney(factoryInitPrice))
            {
                list.Add(new BuildingButtonData(BuildingType.Factory, factoryInitPrice,
                    Localize(_factorySettings.BuildingNameKey)));
            }
        }

        private void AddAutoHarvesterButton(List<BuildingButtonData> list)
        {
            var autoHarvesterInitPrice = GetAutoHarvesterInitPrice();
            if (HaveEnoughMoney(autoHarvesterInitPrice))
            {
                list.Add(new BuildingButtonData(BuildingType.AutoHarvest, autoHarvesterInitPrice,
                    Localize(_autoHarvestSettings.BuildingNameKey)));
            }
        }

        private void AddAutoLighterButton(List<BuildingButtonData> list)
        {
            var autoLighterInitPrice = GetAutoLighterInitPrice();
            if (HaveEnoughMoney(autoLighterInitPrice))
            {
                list.Add(new BuildingButtonData(BuildingType.AutoLighter, autoLighterInitPrice,
                    Localize(_autoLighterSettings.BuildingNameKey)));
            }
        }

        private double GetFactoryInitPrice() => _factorySettings.BuildPrice;

        private double GetAutoHarvesterInitPrice() => _autoHarvestSettings.BuildPrice;

        private double GetAutoLighterInitPrice() => _autoLighterSettings.BuildPrice;

        private string Localize(string key) => _localizationTool.GetText(key);

        private bool HaveEnoughMoney(double price)
        {
            return true; // todo gold service?
        }
    }
}