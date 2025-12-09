using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Buildings
{
    public interface IWorkers
    {
        event Action<List<Unit>> UnitsListChanged;

        List<Unit> CurrentUnits { get; set; }

        void AddSpecUnit(Unit specUnit);

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