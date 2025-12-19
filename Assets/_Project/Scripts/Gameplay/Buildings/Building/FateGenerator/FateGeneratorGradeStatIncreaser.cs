using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    public class FateGeneratorGradeStatIncreaser : MonoBehaviour
    {
        [Inject] private ChurchSettings _churchSettings;
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
            _buildingComponentsInitService.GetGradeData(out var curGradeData, out _,
                _churchSettings.GradeData, newGrade);
            
            _buildingComponentsUpdate.UpdateWorkers(gameObject, curGradeData.MaxUnitsCount);
            _buildingComponentsUpdate.UpdateResourceProducer(gameObject, curGradeData.TimeToProduceFate);
            
        }

    }
}