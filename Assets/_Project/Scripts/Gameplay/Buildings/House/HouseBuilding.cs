using System;

namespace _Project.Scripts.Gameplay.House
{
    public class HouseBuilding : Building
    {
        public Action<HouseBuilding> OnHouseClicked { get; set; }
        public Action<HouseBuilding> OnHouseDestroyed { get; set; }

        private void Awake()
        {
            _durability.OnDestroyed += OnBuildingBroke;
        }

        protected override void HandleButtonClick() => OnHouseClicked?.Invoke(this);

        private void OnBuildingBroke()
        {
            OnHouseDestroyed?.Invoke(this);
            Destroy(gameObject); // todo ?
        }

        private void OnDestroy()
        {
            _durability.OnDestroyed -= OnBuildingBroke;
        }
    }
}