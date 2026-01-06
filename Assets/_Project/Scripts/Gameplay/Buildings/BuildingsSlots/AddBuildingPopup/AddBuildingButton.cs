using System;
using System.Globalization;
using _Project.Scripts.Localization;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class AddBuildingButton : ContentUi
    {
        public Action<BuildingType, double> OnClicked;

        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private ToLocalize _label;

        private BuildingType _buildingType;
        private double _price;

        private void Awake() => _buyButton.onClick.AddListener(OnBuyButtonClicked);
        private void OnDestroy() => _buyButton.onClick.RemoveListener(OnBuyButtonClicked);

        private void OnBuyButtonClicked() => OnClicked?.Invoke(_buildingType, _price);

        public void Init(BuildingButtonData data)
        {
            _buildingType = data.BuildingType;
            _price = data.Price;
            _priceText.text = data.Price.ToString(CultureInfo.InvariantCulture);
            _label.SetKey(data.LabelKey);
            _buyButton.interactable = data.IsEnabled;
        }
    }
}