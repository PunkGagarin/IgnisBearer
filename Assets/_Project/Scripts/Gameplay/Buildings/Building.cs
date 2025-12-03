using _Project.Scripts.Gameplay.Buildings.BuildingComponents.Grade;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents.SpecUnit;
using _Project.Scripts.Gameplay.Buildings.BuildingComponents.SpecUnitsCapacity;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start() => _button.onClick.AddListener(HandleButtonClick);

        private void OnDestroy() => _button.onClick.RemoveListener(HandleButtonClick);

        protected virtual void HandleButtonClick()
        {
            // no-op
        }

        public bool UpdateGrade()
        {
            TryGetComponent<IGrade>(out var grade);
            return grade.UpdateGrade();
            // todo
        }

        public bool SetUnit(Unit unit)
        {
            TryGetComponent<ISpecUnits>(out var units);
            if (CanAddUnit())
            {
                units.AddSpecUnit(unit);
                return true;
            }

            return false;
        }

        private bool CanAddUnit()
        {
            TryGetComponent<IUnitsCapacity>(out var unitsCapacity);
            return unitsCapacity.CanAddUnit();
        }
    }
}