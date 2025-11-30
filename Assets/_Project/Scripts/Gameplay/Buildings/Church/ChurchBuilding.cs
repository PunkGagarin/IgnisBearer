using _Project.Scripts.Gameplay.BuildingComponents.SpecUnit;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Church
{
    public class ChurchBuilding : Building, IResourceGenerator
    {
        public double CurrentResourceCount { get; private set; }

        public void Init() => CurrentResourceCount = 0;

        public bool CanCollect() => CurrentResourceCount > 0;

        public void StartGenerating()
        {
            if (CanGenerate())
                GenerateResource();
        }

        public void StopGenerating()
        {
            Debug.Log("Stop Generating");
        }

        private void GenerateResource()
        {
            Debug.Log("Generating Resource");
        }

        private bool CanGenerate()
        {
            TryGetComponent<SpecUnits>(out var units);
            return units.CurrentUnitsCount > 0;
        }
    }
}