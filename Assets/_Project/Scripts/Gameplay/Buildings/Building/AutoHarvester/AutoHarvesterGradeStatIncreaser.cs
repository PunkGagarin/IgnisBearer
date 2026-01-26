using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class AutoHarvesterGradeStatIncreaser : BaseGradeStatIncreaser
    {
        [Inject] private AutoHarvestSettings _autoHarvesterSettings;
        
        protected override void OnGradeChanged(int newGrade)
        {
            var curGradeData = _autoHarvesterSettings.GetData(newGrade);
            var nextGradeData = _autoHarvesterSettings.GetNextData(newGrade);

            if (nextGradeData == null)
                _grade.ShowGradeMaxed();
            else
                UpdateGrade(gameObject, nextGradeData.GradePrice);
            UpdateDurability(gameObject, curGradeData.MaxDurability);
            UpdateWorkers(gameObject, curGradeData.MaxUnitsCount);
        }
    }
}