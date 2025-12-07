using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class Grade : MonoBehaviour, IGrade
    {
        public event Action<int> GradeChanged;
        public int Current { get; set; }
        public int Max { get; set; }
        
        public float NextGradePrice { get; set; }

        public bool UpdateGrade()
        {
            if (CanUpdate())
            {
                Current++;
                GradeChanged?.Invoke(Current);
                return true;
            }

            return false;
        }

        public void Init(int initValue, int maxValue, float gradePrice)
        {
            Current = initValue;
            Max = maxValue;
            NextGradePrice = gradePrice;
        }

        private bool CanUpdate() => Current < Max && HaveEnoughMoney();
        
        //todo should check HaveEnoughMoney here?
        private bool HaveEnoughMoney() => true;
    }
}