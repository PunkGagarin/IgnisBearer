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
        [Inject] private FateService _fateService;

        public List<BuildingButtonData> GetAddBuildingPopupData()
        {
            List<BuildingButtonData> list = new List<BuildingButtonData>();
            AddBuildingButton(list, GetFactoryInitPrice(), BuildingType.Factory, _factorySettings.BuildingNameKey);
            AddBuildingButton(list, GetAutoHarvesterInitPrice(), BuildingType.AutoHarvest,
                _autoHarvestSettings.BuildingNameKey);
            AddBuildingButton(list, GetAutoLighterInitPrice(), BuildingType.AutoLighter,
                _autoLighterSettings.BuildingNameKey);
            return list;
        }

        private void AddBuildingButton(List<BuildingButtonData> list, float initPrice, BuildingType buildingType,
            string buildingNameKey)
        {
            if (HaveEnoughMoney(initPrice))
                list.Add(new BuildingButtonData(buildingType, initPrice, buildingNameKey));
        }

        private float GetFactoryInitPrice() => _factorySettings.BuildPrice;

        private float GetAutoHarvesterInitPrice() => _autoHarvestSettings.BuildPrice;

        private float GetAutoLighterInitPrice() => _autoLighterSettings.BuildPrice;
        
        private bool HaveEnoughMoney(float price) => _fateService.HasEnough((int)price);
    }
}