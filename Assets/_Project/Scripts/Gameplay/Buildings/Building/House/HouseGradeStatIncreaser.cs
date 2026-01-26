using _Project.Scripts.Gameplay.Ui.Buildings;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class HouseGradeStatIncreaser : BaseGradeStatIncreaser
    {
        [Inject] private HouseSettings _settings;

        protected override void OnGradeChanged(int newGrade)
        {
            var curGradeData = _settings.GetData(newGrade);
            var nextGradeData = _settings.GetNextData(newGrade); 

            if (nextGradeData == null)
                _grade.ShowGradeMaxed();
            else
                UpdateGrade(gameObject, nextGradeData.GradePrice);

            UpdateDurability(gameObject, curGradeData.MaxDurability);
            gameObject.TryGetComponent<HouseBuyUnit>(out var buyUnit);
            buyUnit.SetMaxUnitsCount(curGradeData.MaxUnitsCount);
        }
    }
}