using System;
using _Project.Scripts.Gameplay.Ui.UiEffects;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternSlot : ContentUi
    {
        public event Action OnLanternBought = delegate { };
        public Action<LanternSlot> OnDestroyed = delegate { };

        [Inject] private FateService _fateService;
        [Inject] private LanternService _lanternService;

        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _content;

        private int _lanternCost;

        private void Awake()
        {
            _buyButton.onClick.AddListener(BuyLantern);
            _fateService.OnAmountChanged += OnBalanceChanged;
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(BuyLantern);
            _fateService.OnAmountChanged -= OnBalanceChanged;
            OnDestroyed.Invoke(this);
        }

        public void Init(int cost)
        {
            _lanternCost = cost;
            UpdateUi();
        }

        public void SetLanternCost(int cost)
        {
            _lanternCost = cost;
            UpdateUi();
        }

        private void OnBalanceChanged((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            UpdateUi();
        }

        private void UpdateUi()
        {
            _buyButton.GetComponent<UiButtonEnableEffect>().SetInteractable(CanBuyLantern());
            _costText.text = _lanternCost.ToString();
        }

        private bool CanBuyLantern()
        {
            return _fateService.HasEnough(_lanternCost);
        }

        private void BuyLantern()
        {
            _fateService.Spend(_lanternCost);
            _lanternService.CreateAndRegisterLantern(this);
            OnLanternBought?.Invoke();
            Hide();
        }
    }
}