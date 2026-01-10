using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    [RequireComponent(typeof(Grade))]
    [RequireComponent(typeof(IWorkers))]
    [RequireComponent(typeof(ResourceProducer))]
    public class FateGeneratorGradeStatIncreaser : BaseGradeStatIncreaser
    {
        [Inject] private FateGeneratorSettings _fateGeneratorSettings;

        protected override void OnGradeChanged(int newGrade)
        {
            var curGradeData = _fateGeneratorSettings.GetData(newGrade);
            var nextGradeData = _fateGeneratorSettings.GetNextData(newGrade); 
            
            if (nextGradeData == null)
                _grade.HideBuyButton();
            else
                UpdateGrade(gameObject, nextGradeData.GradePrice);

            UpdateWorkers(gameObject, curGradeData.MaxUnitsCount);
            UpdateResourceProducer(gameObject, curGradeData.TimeToProduceFate);
            UpdateWorkerProduceAmountIncreaser(curGradeData);
        }

        private void UpdateWorkerProduceAmountIncreaser(FateGeneratorGradeData curGradeData)
        {
            var workerProduceAmountIncreaser = gameObject.GetComponent<WorkerProduceAmountIncreaser>();
            workerProduceAmountIncreaser.Init(curGradeData.AmountToProduceFate);
        }
    }
}