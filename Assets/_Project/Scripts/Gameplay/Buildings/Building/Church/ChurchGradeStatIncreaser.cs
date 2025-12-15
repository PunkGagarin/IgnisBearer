using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class ChurchGradeStatIncreaser : MonoBehaviour
    {
        [Inject] private ChurchSettings _churchSettings;
        [Inject] private LightConsumeSettings _lightConsumeSettings;
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
            _buildingComponentsInitService.GetGradeData(out var curGradeData, out var nextGradeData, _churchSettings.GradeData, newGrade);

            if (nextGradeData == null)
                _grade.HideBuyButton();
            else
                _buildingComponentsUpdate.UpdateGrade(gameObject, nextGradeData.GradePrice);
            
            _buildingComponentsUpdate.UpdateResourceStorage(gameObject, curGradeData.MaxLightStorageCapacity);
            
        }

    }
}