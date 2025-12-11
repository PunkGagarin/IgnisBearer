using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IGrade
    {
        event Action<int> OnGradeChanged;
        int Current { get; set; }
        float NextGradePrice { get; set; }
        bool UpdateGrade();
        void Init(int initValue, float gradePrice);
    }
}