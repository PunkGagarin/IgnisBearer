using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class FactoryGradeStatIncreaser : BaseGradeStatIncreaser
    {
        [Inject] private FactorySettings _settings;


        protected override void OnGradeChanged(int newGrade)
        {
            var curGradeData = _settings.GetData(newGrade);
            var nextGradeData = _settings.GetNextData(newGrade);

            if (nextGradeData == null)
                _grade.ShowGradeMaxed();
            else
                UpdateGrade(gameObject, nextGradeData.GradePrice);
            UpdateDurability(gameObject, curGradeData.MaxDurability);
            UpdateWorkers(gameObject, curGradeData.MaxUnitsCount);
        }
    }
}