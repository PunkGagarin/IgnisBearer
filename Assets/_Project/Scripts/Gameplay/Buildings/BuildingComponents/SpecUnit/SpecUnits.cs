using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.BuildingComponents.SpecUnit
{
    public class SpecUnits : MonoBehaviour, ISpecUnits
    {
        public event Action<List<Unit>> UnitsListChanged;

        public List<Unit> CurrentUnits { get; set; }

        public void Init()
        {
            CurrentUnits = new List<Unit>();
        }

        public void AddSpecUnit(Unit specUnit)
        {
            CurrentUnits.Add(specUnit);
            UnitsListChanged?.Invoke(CurrentUnits);
        }
    }
}