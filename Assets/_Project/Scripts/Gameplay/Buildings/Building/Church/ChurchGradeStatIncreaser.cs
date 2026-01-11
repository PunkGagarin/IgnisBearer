using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchGradeStatIncreaser : BaseGradeStatIncreaser
    {
        [Inject] private ChurchSettings _churchSettings;

        protected override void OnGradeChanged(int newGrade)
        {
            var curGradeData = _churchSettings.GetData(newGrade);
            var nextGradeData = _churchSettings.GetNextData(newGrade); 

            if (nextGradeData == null)
                _grade.HideBuyButton();
            else
                UpdateGrade(gameObject, nextGradeData.GradePrice);
            UpdateResourceStorage(gameObject, curGradeData.MaxLightStorageCapacity);
            UpdateQueueCapacity(gameObject, curGradeData.QueueCapacity);
        }

        private void UpdateQueueCapacity(GameObject building, int queueCapacity)
        {
            building.TryGetComponent<ChurchQueue>(out var queue);
            queue.Init(queueCapacity);
        }
    }
}