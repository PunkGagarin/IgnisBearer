using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.BuildingComponents.SpecUnit
{
    public interface ISpecUnits
    {
        event Action<int> UnitsCountChanged;

        int CurrentUnitsCount { get; set; }
        int MaxUnitsCount { get; set; }
        List<PeonUnit> CurrentUnits { get; set; }

        void Init(int initValue, int maxValue);
        bool AddSpecUnit(PeonUnit specUnit);
    }
}