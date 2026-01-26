using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IGrade
    {
        event Action<int> OnGradeChanged;
        int Current { get; set; }
        int NextGradePrice { get; set; }
        void UpgradeGrade();
        void Init(int initValue, int maxGrade, int gradePrice);
        void SetNextGradePrice(int nextGradePrice);
        void ShowGradeMaxed();
        bool CanUpdate();
    }
}