using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingAddingOptionsService
    {
        [Inject] private FateService _fateService;
        [Inject] private BuildingsService _buildingsService;
        [Inject] private BuildingSettings _buildingSettings;
        [Inject] private List<IBuildInfo> _buildInfos;

        public List<BuildingButtonData> GetAddBuildingPopupData()
        {
            List<BuildingButtonData> list = new List<BuildingButtonData>();

            AddBuildingButtonFor(BuildingType.House, list);
            var isHouseBuilt = _buildingsService.GetBuildingCountByType(BuildingType.FateGenerator) > 0;
            if (!isHouseBuilt)
                return list;


            AddBuildingButtonFor(_buildingSettings.FirstToBuyBuilding, list);

            var isFateGenBuilt = _buildingsService.GetBuildingCountByType(BuildingType.FateGenerator) > 0;

            if (isFateGenBuilt)
            {
                foreach (var building in _buildingSettings.AvailableToBuildBuildings)
                    AddBuildingButtonFor(building, list);
            }

            return list;
        }

        private void AddBuildingButtonFor(BuildingType type, List<BuildingButtonData> list)
        {
            IBuildInfo buildInfo = GetSettingsFor(type);
            AddBuildingButton(type, list, buildInfo);
        }

        private IBuildInfo GetSettingsFor(BuildingType type)
        {
            var buildInfo = _buildInfos.FirstOrDefault(el => el.Type == type);
            if (buildInfo == null)
            {
                Debug.LogError($" Нету настроек для типа: {type}");
                return _buildInfos[0];
            }

            return buildInfo;
        }

        private void AddBuildingButton(BuildingType buildingType, List<BuildingButtonData> list, IBuildInfo buildInfo)
        {
            if (CanHaveAnother(buildingType, buildInfo.MaxCountToBuild))
                list.Add(
                    new BuildingButtonData(
                        buildingType,
                        buildInfo.BuildPrice,
                        buildInfo.BuildingNameKey,
                        HaveEnoughMoney(buildInfo.BuildPrice)
                    )
                );
        }

        private bool CanHaveAnother(BuildingType buildingType, int maxCount)
        {
            var curCount = _buildingsService.GetBuildingCountByType(buildingType);
            return curCount < maxCount;
        }

        private bool HaveEnoughMoney(float price) => _fateService.HasEnough((int)price);
    }
}