using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.BuildingComponents.SpecUnit
{
    public interface ISpecUnits
    {
        event Action<List<Unit>> UnitsListChanged;

        List<Unit> CurrentUnits { get; set; }

        void AddSpecUnit(Unit specUnit);

        void Init();
    }
}