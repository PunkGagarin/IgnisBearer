using System;

namespace _Project.Scripts.Gameplay.BuildingComponents.Grade
{
    public interface IGrade
    {
        event Action<int> GradeChanged;
        int Current { get; set; }
        int Max { get; set; }
        float NextGradePrice { get; set; }
        bool UpdateGrade();
        void Init(int initValue, int maxValue, float gradePrice);
    }
}