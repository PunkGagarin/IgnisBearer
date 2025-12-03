using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.BuildingComponents.SpecUnitsCapacity
{
    public class UnitsCapacity : MonoBehaviour, IUnitsCapacity
    {
        public event Action<int> UnitsCountChanged;
        public event Action<int> MaxUnitsCountChanged;

        public int Current { get; set; }
        public int Max { get; set; }

        public void UpdateMaxCount(int count)
        {
            Max = count;
            MaxUnitsCountChanged?.Invoke(count);
        }

        public void UpdateCount(int count)
        {
            Current = count;
            UnitsCountChanged?.Invoke(count);
        }

        public void Init(int initValue, int maxValue)
        {
            Current = initValue;
            Max = maxValue;
        }

        public bool CanAddUnit() => Current < Max;
    }
}