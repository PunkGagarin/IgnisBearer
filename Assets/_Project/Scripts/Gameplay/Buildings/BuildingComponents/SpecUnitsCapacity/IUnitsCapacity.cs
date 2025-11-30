using System;

namespace _Project.Scripts.Gameplay.BuildingComponents.SpecUnit
{
    public interface IUnitsCapacity
    {
        event Action<int> UnitsCountChanged;
        event Action<int> MaxUnitsCountChanged;
        int Current { get; set; }
        int Max { get; set; }
        void UpdateMaxCount(int count);
        void UpdateCount(int count);
        void Init(int initValue, int maxValue);
        bool CanAddUnit();
    }
}