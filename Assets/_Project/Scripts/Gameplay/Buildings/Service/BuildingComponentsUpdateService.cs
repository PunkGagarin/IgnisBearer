using _Project.Scripts.Gameplay.Ui.Buildings;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingComponentsUpdateService
    {
        public void UpdateLightConsumer(GameObject building, int time, int amount)
        {
        }

        public void UpdateResourceStorage(GameObject building, int maxStorageCapacity)
        {
            building.TryGetComponent<IResourceStorage>(out var storage);
            storage.Init(maxStorageCapacity);
        }

        public void UpdateGrade(GameObject building, int gradePrice)
        {
            building.TryGetComponent<IGrade>(out var grade);
            grade.SetNextGradePrice(gradePrice);
        }

        public void UpdateResourceProducer(GameObject building, int timeToProduceFate)
        {
            var fateProducer = building.GetComponent<ResourceProducer>();
            fateProducer.Init(timeToProduceFate);
        }

        public void UpdateWorkers(GameObject building, int maxUnitsCount)
        {
            building.TryGetComponent<IWorkers>(out var workers);
            workers.SetMaxUnitCount(maxUnitsCount);
        }

        public void UpdateDurability(GameObject building, int maxDurability)
        {
            building.TryGetComponent<IDurability>(out var durability);
            durability.SetMaxValue(maxDurability);
        }

        public void UpdateBuyUnitHouse(GameObject building, int maxUnitCount)
        {
            building.TryGetComponent<HouseBuyUnit>(out var buyUnit);
            buyUnit.SetMaxUnitsCount(maxUnitCount);
        }
    }
}