using System;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class HouseBuilding : Building
    {
        public Action<HouseBuilding> OnHouseClicked { get; set; }
        public Action<HouseBuilding> OnHouseDestroyed { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _durability.OnDestroyed += OnBuildingBroke;
        }

        protected override void HandleButtonClick() => OnHouseClicked?.Invoke(this);

        private void OnBuildingBroke()
        {
            _durability.OnDestroyed -= OnBuildingBroke;
            OnHouseDestroyed?.Invoke(this);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (_durability != null)
                _durability.OnDestroyed -= OnBuildingBroke;
        }
    }
}