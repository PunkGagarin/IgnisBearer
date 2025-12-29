using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class AutoHarvesterGradeStatIncreaser : MonoBehaviour
    {
        [Inject] private AutoHarvestSettings _autoHarvesterSettings;
        [Inject] private BuildingComponentsInitService _buildingComponentsInitService;
        [Inject] private BuildingComponentsUpdateService _buildingComponentsUpdate;

        private Grade _grade;

        private void Awake()
        {
            _grade = GetComponent<Grade>();
            _grade.OnGradeChanged += OnGradeChanged;
        }

        private void OnDestroy()
        {
            _grade.OnGradeChanged -= OnGradeChanged;
        }

        private void OnGradeChanged(int newGrade)
        {
            _buildingComponentsInitService.GetGradeData(out var curGradeData, out var nextGradeData,
                _autoHarvesterSettings.GradeData, newGrade);

            if (nextGradeData == null)
                _grade.HideBuyButton();
            else
                _buildingComponentsUpdate.UpdateGrade(gameObject, nextGradeData.GradePrice);
            _buildingComponentsUpdate.UpdateDurability(gameObject, curGradeData.MaxDurability);
            _buildingComponentsUpdate.UpdateWorkers(gameObject, curGradeData.MaxUnitsCount);
        }

    }
}