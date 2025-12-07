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

        void Init();
    }
}