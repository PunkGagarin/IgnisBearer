using System;
using _Project.Scripts.Gameplay.BuildingComponents;
using _Project.Scripts.Gameplay.BuildingComponents.Durability;
using UnityEngine;

namespace _Project.Scripts.Gameplay.House
{
    public class HouseBuilding : Building
    {
        public Action<HouseBuilding> OnHouseClicked { get; set; }
        public Action<HouseBuilding> OnHouseDestroyed { get; set; }

        private void Awake()
        {
            TryGetComponent<BuildingClick>(out var buildingClick);
            buildingClick.OnClicked += OnClicked;
            
            TryGetComponent<Durability>(out var durability);
            durability.OnDestroyed += OnDestroyed;
        }

        private void OnDestroyed() => OnHouseDestroyed?.Invoke(this);

        private void OnClicked() => OnHouseClicked?.Invoke(this);

        private void OnDestroy()
        {
            TryGetComponent<BuildingClick>(out var buildingClick);
            buildingClick.OnClicked -= OnClicked;
            
            TryGetComponent<Durability>(out var durability);
            durability.OnDestroyed += OnDestroyed;
            
        }
    }
}