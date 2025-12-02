using System;
using _Project.Scripts.Gameplay.BuildingComponents.Grade;
using _Project.Scripts.Gameplay.BuildingComponents.Workers;
using _Project.Scripts.Gameplay.BuildingComponents.WorkersCapacity;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay
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
            TryGetComponent<IWorkers>(out var units);
            if (CanAddUnit())
            {
                units.AddSpecUnit(unit);
                return true;
            }

            return false;
        }

        private bool CanAddUnit()
        {
            TryGetComponent<IWorkersCapacity>(out var unitsCapacity);
            return unitsCapacity.CanAddUnit();
        }
    }
}