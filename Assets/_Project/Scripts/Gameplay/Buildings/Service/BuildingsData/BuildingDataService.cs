using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsData
{
    public class BuildingDataService
    {
        [Inject] private readonly PlayerDataService _playerDataService;
        [Inject] private readonly BuildingSlotsService _buildingSlotsService;
        [Inject] private readonly BuildingsService _buildingsService;

        void SaveChurch(ChurchBuilding building)
        {
            var workersCount = GetWorkersCount(building);
            var resourceCount = GetResourceCount(building);
            var grade = GetCurrentGrade(building);
            var data = new ChurchBuildingData
            {
                CurrentGrade = grade,
                WorkersCount = workersCount,
                ResourceCount = resourceCount
            };
            _playerDataService.PlayerData.BuildingsContext.ChurchContext.BuildingsData = data;
        }


        public void RestoreChurch(BuildingSlotsSpawnPoint spawnPoint)
        {
            var church = LoadChurchData();
            RestoreBuilding(spawnPoint, church);
        }

        public void RestoreOtherBuildings(List<BuildingSlotsSpawnPoint> spawnPoints)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                RestoreBuildings(spawnPoint, LoadHouseData());
                /*RestoreBuildings(spawnPoint, LoadAutoHarvetersData());
                RestoreBuildings(spawnPoint, LoadAutoLightersData());
                RestoreBuildings(spawnPoint, LoadFactoriesData());
                RestoreBuildings(spawnPoint, LoadFateGeneratorsData());*/
            }
        }

        private void RestoreBuildings<T>(BuildingSlotsSpawnPoint spawnPoint, List<T> buildingsData)
            where T : BuildingData
        {
            foreach (var data in buildingsData)
            {
                RestoreBuilding(spawnPoint, data);
            }
        }

        private void RestoreBuilding(BuildingSlotsSpawnPoint spawnPoint, BuildingData data)
        {
            var slot = _buildingSlotsService.RestoreSlotWithId(spawnPoint, data.SlotId);
            _buildingsService.RestoreBuildingAt(data, slot);
        }

        private ChurchBuildingData LoadChurchData()
        {
            return _playerDataService.PlayerData.BuildingsContext.ChurchContext.BuildingsData;
        }

        private List<HouseBuildingData> LoadHouseData()
        {
            return _playerDataService.PlayerData.BuildingsContext.HouseContext.BuildingsData;
        }

        private static int GetCurrentGrade(ChurchBuilding building)
        {
            return building.GetComponent<IGrade>().Current;
        }

        private static int GetResourceCount(ChurchBuilding building)
        {
            return building.GetComponent<IResourceStorage>().Amount;
        }

        private static int GetWorkersCount(ChurchBuilding building)
        {
            return building.GetComponent<IWorkers>().CurrentCount;
        }
    }
}