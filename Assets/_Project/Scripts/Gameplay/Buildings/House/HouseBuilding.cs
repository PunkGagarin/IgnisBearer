using System;
using _Project.Scripts.Gameplay.BuildingComponents.Durability;

namespace _Project.Scripts.Gameplay.House
{
    public class HouseBuilding : Building
    {
        public Action<HouseBuilding> OnHouseClicked { get; set; }
        public Action<HouseBuilding> OnHouseDestroyed { get; set; }

        private void Awake()
        {
            TryGetComponent<IDurability>(out var durability);
            durability.OnDestroyed += OnBuildingBroke;
        }

        protected override void HandleButtonClick() => OnHouseClicked?.Invoke(this);

        private void OnBuildingBroke()
        {
            OnHouseDestroyed?.Invoke(this);
            Destroy(gameObject); // todo ?
        }

        private void OnDestroy()
        {
            TryGetComponent<Durability>(out var durability);
            durability.OnDestroyed -= OnBuildingBroke;
        }
    }
}