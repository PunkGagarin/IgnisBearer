using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class Workers : MonoBehaviour, IWorkers
    {
        public event Action<List<Unit>> UnitsListChanged;
        public event Action<int> UnitsCountChanged;
        public event Action<int> MaxUnitsCountChanged;

        public List<Unit> CurrentUnits { get; set; } = new();
        public int Current { get; set; }

        public int Max { get; set; }

        public void Init(int initValue, int maxValue)
        {
            Current = initValue;
            Max = maxValue;
        }

        public void UpdateMaxCount(int count)
        {
            Max = count;
            MaxUnitsCountChanged?.Invoke(count);
        }

        public void IncrementCount()
        {
            UpdateCount(1);
        }

        public void UpdateCount(int count)
        {
            Current = count;
            UnitsCountChanged?.Invoke(count);
        }

        public bool CanAddUnit()
            => Current < Max;

        public void AddSpecUnit(Unit specUnit)
        {
            if (CanAddUnit())
            {
                Debug.LogError("Workers capacity is full");
                return;
            }

            CurrentUnits.Add(specUnit);
            IncrementCount();
            UnitsListChanged?.Invoke(CurrentUnits);
        }
    }
}