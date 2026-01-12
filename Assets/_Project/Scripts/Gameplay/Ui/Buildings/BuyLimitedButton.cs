using System;
using _Project.Scripts.Gameplay.Ui.UiEffects;
using _Project.Scripts.Localization;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Ui.Buildings
{
    public class BuyLimitedButton : ContentUi
    {
        public event Action OnBuyClicked = delegate { };

        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private ToLocalize _itemTextToLocalize;

        private void Awake()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        }

        private void OnBuyButtonClicked() => OnBuyClicked?.Invoke();

        public void UpdateUi(string itemNameKey, string countText, bool isButtonEnabled, string costText)
        {
            _itemTextToLocalize.SetKey(itemNameKey);
            _countText.text = countText;
            _buyButton.GetComponent<UiButtonEnableEffect>().SetInteractable(isButtonEnabled);
            _costText.text = costText;
        }

        public void ShowButton(bool show) => 
            _buyButton.gameObject.SetActive(show);
    }
}