using System;
using _Project.Scripts.Gameplay.BuildingComponents.WorkersCapacity;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Church
{
    public class ChurchBuilding : Building, IResourceGenerator
    {
        public Action<ChurchBuilding> OnChurchClicked = delegate { };
        public Action<ChurchBuilding> OnChurchDestroyed = delegate { };

        public double CurrentResourceCount { get; private set; }

        private IWorkersCapacity _workersCapacity;

        private void Awake()
        {
            _durability.OnDestroyed += OnBuildingBroke;
            _workersCapacity = GetComponent<IWorkersCapacity>();
        }

        private void OnBuildingBroke()
        {
            OnChurchDestroyed?.Invoke(this);
            Destroy(gameObject); // todo ?
        }

        private void OnDestroy()
        {
            _durability.OnDestroyed -= OnBuildingBroke;
        }

        protected override void HandleButtonClick() => OnChurchClicked?.Invoke(this);

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