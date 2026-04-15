using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Ui.UiEffects;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class AddBuildingPopup : ContentUi
    {
        public event Action<BuildingType, double> OnAddBuilding;

        [SerializeField] private List<AddBuildingButton> _addBuildingsButton;

        private UiPopupDisplayer _popupDisplayer;

        private void Awake()
        {
            _popupDisplayer = content.GetComponent<UiPopupDisplayer>();
            _addBuildingsButton.ForEach(btn => btn.OnClicked += OnAddBuildingClicked);
        }

        private void OnDestroy()
        {
            _addBuildingsButton.ForEach(btn => btn.OnClicked -= OnAddBuildingClicked);
        }

        public void SetData(List<BuildingButtonData> buildingButtonsData)
        {
            _addBuildingsButton.ForEach(btn => btn.Hide());

            var buttonsToShow = Mathf.Min(buildingButtonsData.Count, _addBuildingsButton.Count);
            if (buildingButtonsData.Count > _addBuildingsButton.Count)
            {
                Debug.LogError(
                    $"Not enough add building buttons configured. Need {buildingButtonsData.Count}, have {_addBuildingsButton.Count}");
            }

            for (var i = 0; i < buttonsToShow; i++)
            {
                _addBuildingsButton[i].Show();
                _addBuildingsButton[i].UpdateUi(buildingButtonsData[i]);
            }
        }

        public override void Hide()
        {
            _popupDisplayer.AnimateAndHide();
        }

        private void OnAddBuildingClicked(BuildingType buildingType, double price) =>
            OnAddBuilding?.Invoke(buildingType, price);
    }
}
