using System;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Buildings
{
    internal class BuyUnitsPopup : ContentUi
    {
        public event Action<float> OnBuyClicked = delegate { };

        // [Inject] private GoldService _goldService;

        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private Button _buyButton;

        [SerializeField]
        private TMP_Text _priceText;

        private float _unitPrice;

        private void Awake()
        {
            _closeButton.onClick.AddListener(Hide);
            _buyButton.onClick.AddListener(OnBuyUnitClicked);
            // _goldService.OnBalanceChanged += OnBalanceChanged;
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Hide);
            _buyButton.onClick.RemoveListener(OnBuyUnitClicked);
            // _goldService.OnBalanceChanged -= OnBalanceChanged;
        }

        public void Init(float unitPrice)
        {
            _unitPrice = unitPrice;
            _priceText.text = $"{unitPrice}";
            UpdateBuyButton(unitPrice);
        }

        private void OnBuyUnitClicked() => OnBuyClicked?.Invoke(_unitPrice);

        private void UpdateBuyButton(float unitPrice)
        {
            _buyButton.interactable = HaveEnoughMoney(unitPrice);
        }

        /*private void OnBalanceChanged(float balance)
        {
            UpdateBuyButton(_unitPrice);
        }*/

        private bool HaveEnoughMoney(float unitPrice)
        {
            //todo gold service
            return true;
        }
    }

}