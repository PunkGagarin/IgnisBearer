using _Project.Scripts.Gameplay.Buildings.BuildingComponents.WorkersCapacity;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Church
{
    public class ChurchBuilding : Building, IResourceGenerator
    {
        public double CurrentResourceCount { get; private set; }

        private IWorkersCapacity _workersCapacity;

        private void Awake()
        {
            _workersCapacity = GetComponent<IWorkersCapacity>();
        }

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
            return _workersCapacity.Current > 0;
        }
    }
}