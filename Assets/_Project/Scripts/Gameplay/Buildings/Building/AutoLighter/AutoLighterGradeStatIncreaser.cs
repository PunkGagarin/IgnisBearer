using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class AutoLighterGradeStatIncreaser : BaseGradeStatIncreaser
    {
        [Inject] private AutoLighterSettings _autoLighterSettings;

        protected override void OnGradeChanged(int newGrade)
        {
            var curGradeData = _autoLighterSettings.GetData(newGrade);
            var nextGradeData = _autoLighterSettings.GetNextData(newGrade); 

            if (nextGradeData == null)
                _grade.ShowGradeMaxed();
            else
                UpdateGrade(gameObject, nextGradeData.GradePrice);
            UpdateDurability(gameObject, curGradeData.MaxDurability);
            UpdateWorkers(gameObject, curGradeData.MaxUnitsCount);
        }

    }
}