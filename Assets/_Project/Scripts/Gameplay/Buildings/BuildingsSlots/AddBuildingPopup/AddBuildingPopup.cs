using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Ui;
using _Project.Scripts.Gameplay.Ui.UiEffects;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class AddBuildingPopup : ContentUi
    {
        public event Action<BuildingType> OnAddBuilding;

        [SerializeField] private List<AddBuildingButton> _addBuildingsButton;

        private UiPopupDisplayer _popupDisplayer;

        private void Awake()
        {
            _popupDisplayer = content.GetComponent<UiPopupDisplayer>();
        }

        private void OnDestroy()
        {
            _addBuildingsButton.ForEach(btn => btn.OnClicked += OnAddBuildingClicked);
        }

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

        public override void Hide()
        {
            _popupDisplayer.AnimateAndHide();
        }

        private void OnAddBuildingClicked(BuildingType buildingType, double price) =>
            OnAddBuilding?.Invoke(buildingType);
    }
}