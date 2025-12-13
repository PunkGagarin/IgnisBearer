using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class Grade : MonoBehaviour, IGrade
    {
        public event Action<int> OnGradeChanged;
        public int Current { get; set; }

        public float NextGradePrice { get; set; }

        public bool UpdateGrade()
        {
            if (CanUpdate())
            {
                Current++;
                OnGradeChanged?.Invoke(Current);
                return true;
            }

            return false;
        }

        public void Init(int initValue, float gradePrice)
        {
            Current = initValue;
            NextGradePrice = gradePrice;
        }

        private bool CanUpdate() => HaveEnoughMoney();

        //todo should check HaveEnoughMoney here?
        private bool HaveEnoughMoney() => true;
    }

}