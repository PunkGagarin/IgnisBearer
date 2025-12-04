using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.BuildingsSlots
{
    public class AddBuildingPopup : ContentUi
    {
        public event Action<BuildingType> OnAddBuilding;

        [SerializeField] private Button _closeButton;
        [SerializeField] private List<AddBuildingButton> _addBuildingsButton;

        private void Awake() => _closeButton.onClick.AddListener(OnCloseButtonClicked);

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
            _addBuildingsButton.ForEach(btn => btn.OnClicked += OnAddBuildingClicked);
        }

        private void OnCloseButtonClicked() => Hide();

        public void Init(List<BuildingButtonData> buildingButtonsData)
        {
            _addBuildingsButton.ForEach(btn => btn.Hide());
            for (var i = 0; i < buildingButtonsData.Count; i++)
            {
                _addBuildingsButton[i].Show();
                _addBuildingsButton[i].Init(buildingButtonsData[i]);
                _addBuildingsButton[i].OnClicked += OnAddBuildingClicked;
            }
        }

        private void OnAddBuildingClicked(BuildingType buildingType, double price) =>
            OnAddBuilding?.Invoke(buildingType);
    }
}