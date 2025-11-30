using _Project.Scripts.Gameplay.BuildingComponents.Grade;
using _Project.Scripts.Gameplay.BuildingComponents.SpecUnit;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class Building : MonoBehaviour
    {
        public bool UpdateGrade()
        {
            TryGetComponent<Grade>(out var grade);
            return grade.UpdateGrade();
            // todo
        }

        public bool SetUnit(PeonUnit unit)
        {
            TryGetComponent<SpecUnits>(out var units);
            return units.AddSpecUnit(unit);
        }
    }
}