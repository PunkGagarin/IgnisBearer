using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    public class FateGeneratorFactory : BuildingFactory<FateGeneratorBuilding, FateGeneratorSettings, FateGeneratorGradeData>
    {
        public override FateGeneratorBuilding Create(BuildingSlot slot, int grade)
        {
            var building = InstantiateBuildingOnSlot(slot, _settings.Prefab);
            var gradeData = _settings.GetData(grade);
            var nextGradeData = _settings.GetNextData(grade);

            InitGradeComponent(building, grade, _settings.MaxGrade, nextGradeData.GradePrice);
            InitWorkersComponent(building.gameObject, gradeData.MaxUnitsCount);

            var fateResourceStorage = building.GetComponent<IResourceStorage>();
            fateResourceStorage.Init(int.MaxValue);

            var fateProducer = building.GetComponent<ResourceProducer>();
            fateProducer.Init(gradeData.TimeToProduceFate);

            var workerProduceAmountIncreaser = building.GetComponent<WorkerProduceAmountIncreaser>();
            workerProduceAmountIncreaser.Init(gradeData.FateAmountPerWorker);
            
            slot.SetEnabled(false);
            return building;
        }
    }
}