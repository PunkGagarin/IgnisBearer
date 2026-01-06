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
            //disabled building factory
            /*AddBuildingButton(BuildingType.Factory, list, GetFactoryInitPrice(), _factorySettings.BuildingNameKey,
                _factorySettings.MaxCountToBuild);*/
            
            AddBuildingButton(BuildingType.FateGenerator, list, GetFateGenInitPrice(),
                _fateGeneratorSettings.BuildingNameKey, _fateGeneratorSettings.MaxCountToBuild);

            var isFateGenBuilt = _buildingsService.GetBuildingCountByType(BuildingType.FateGenerator) > 0;

            if (isFateGenBuilt)
                AddBuildingButton(BuildingType.AutoHarvest, list, GetAutoHarvesterInitPrice(),
                    _autoHarvestSettings.BuildingNameKey, _autoHarvestSettings.MaxCountToBuild);

            if (isFateGenBuilt)
                AddBuildingButton(BuildingType.AutoLighter, list, GetAutoLighterInitPrice(),
                    _autoLighterSettings.BuildingNameKey, _autoLighterSettings.MaxCountToBuild);

            return list;
        }

        private void AddBuildingButton(BuildingType buildingType, List<BuildingButtonData> list, float initPrice,
            string buildingNameKey, int maxBuildingCount)
        {
            if (CanHaveAnother(buildingType, maxBuildingCount))
                list.Add(
                    new BuildingButtonData(
                        buildingType,
                        initPrice,
                        buildingNameKey,
                        HaveEnoughMoney(initPrice)
                    )
                );
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