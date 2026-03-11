using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class GradeSettings<TBuilding, TGradeData> : ScriptableObject
    {
        [SerializeField] protected TBuilding _prefab;
        [SerializeField] protected List<TGradeData> _gradeData;

        public TBuilding Prefab => _prefab;
        protected List<TGradeData> GradeData => _gradeData;

        public TGradeData GetData(int grade)
        {
            var gradeIndex = grade - 1;
            if (gradeIndex >= 0 && gradeIndex <= GradeData.Count)
            {
                return GradeData[gradeIndex];
            }

            Debug.LogError($"Invalid grade index: {gradeIndex}");
            return GradeData[0];
        }

        public TGradeData GetNextData(int grade)
        {
            var gradeIndex = grade - 1;
            int nextIndex = gradeIndex + 1;
            if (nextIndex > 0 && nextIndex < GradeData.Count)
            {
                return GradeData[nextIndex];
            }

            return default;
        }
    }
}