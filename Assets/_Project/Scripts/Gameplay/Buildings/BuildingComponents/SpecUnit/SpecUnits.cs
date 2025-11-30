using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.BuildingComponents.SpecUnit
{
    public class SpecUnits : MonoBehaviour, ISpecUnits
    {
        public event Action<int> UnitsCountChanged;

        public int CurrentUnitsCount { get; set; }
        public int MaxUnitsCount { get; set; }
        public List<PeonUnit> CurrentUnits { get; set; }

        public void Init(int initValue, int maxValue)
        {
            CurrentUnits = new List<PeonUnit>();
            CurrentUnitsCount = initValue;
            MaxUnitsCount = maxValue;
        }

        public bool AddSpecUnit(PeonUnit specUnit)
        {
            if (CanAddUnit())
            {
                CurrentUnits.Add(specUnit);
                CurrentUnitsCount++;
                UnitsCountChanged?.Invoke(CurrentUnitsCount);
                return true;
            }

            return false;
        }

        private bool CanAddUnit() => CurrentUnitsCount ! >= MaxUnitsCount;
    }
}