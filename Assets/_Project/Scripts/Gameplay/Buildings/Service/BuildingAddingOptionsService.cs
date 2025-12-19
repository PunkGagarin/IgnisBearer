using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using _Project.Scripts.Gameplay.Buildings.FateGenerator;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingAddingOptionsService
    {
        [Inject] private FactorySettings _factorySettings;
        [Inject] private AutoHarvestSettings _autoHarvestSettings;
        [Inject] private AutoLighterSettings _autoLighterSettings;
        [Inject] private FateGeneratorSettings _fateGeneratorSettings;
        [Inject] private FateService _fateService;
        [Inject] private BuildingsService _buildingsService;

        public List<BuildingButtonData> GetAddBuildingPopupData()
        {
            List<BuildingButtonData> list = new List<BuildingButtonData>();
            AddBuildingButton(list, GetFactoryInitPrice(), BuildingType.Factory, _factorySettings.BuildingNameKey,
                _factorySettings.MaxCountToBuild);
            AddBuildingButton(list, GetAutoHarvesterInitPrice(), BuildingType.AutoHarvest,
                _autoHarvestSettings.BuildingNameKey, _autoHarvestSettings.MaxCountToBuild);
            AddBuildingButton(list, GetAutoLighterInitPrice(), BuildingType.AutoLighter,
                _autoLighterSettings.BuildingNameKey, _autoLighterSettings.MaxCountToBuild);
            AddBuildingButton(list, GetFateGenInitPrice(), BuildingType.FateGenerator,
                _fateGeneratorSettings.BuildingNameKey, _fateGeneratorSettings.MaxCountToBuild);
            return list;
        }

        private void AddBuildingButton(List<BuildingButtonData> list, float initPrice, BuildingType buildingType,
            string buildingNameKey, int maxBuildingCount)
        {
            if (HaveEnoughMoney(initPrice) && CanHaveAnother(buildingType, maxBuildingCount))
                list.Add(new BuildingButtonData(buildingType, initPrice, buildingNameKey));
        }

        private bool CanHaveAnother(BuildingType buildingType, int maxCount)
        {
            var curCount = _buildingsService.GetBuildingCountByType(buildingType);
            return curCount < maxCount;
        }

        private float GetFateGenInitPrice() => _fateGeneratorSettings.BuildPrice;

        private float GetFactoryInitPrice() => _factorySettings.BuildPrice;

        private float GetAutoHarvesterInitPrice() => _autoHarvestSettings.BuildPrice;

        private float GetAutoLighterInitPrice() => _autoLighterSettings.BuildPrice;

        private bool HaveEnoughMoney(float price) => _fateService.HasEnough((int)price);
    }
}