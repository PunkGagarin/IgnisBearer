using System;
using System.Globalization;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Buildings
{
    internal class HouseBuyUnit : MonoBehaviour
    {
        private const string HOUSE_BUY_UNIT_ITEM_DESC_KEY = "HOUSE_BUY_UNIT_ITEM_DESC";

        [Inject] private WorkerService _workerService;
        [Inject] private HouseSettings _houseSettings;
        [Inject] private FateService _fateService;
        [Inject] private BuildingsService _buildingsService;

        [SerializeField] private BuyLimitedButton _buyUnitButton;

        public int MaxUnitsCount  { get; private set; }
        public int UnitsCount  { get; private set; }
        
        private int _unitPrice;
        private int _unitCostMultiplier;
        
        public event Action<int> OnUnitCountChanged = delegate { };
        public event Action<int> OnMaxCountChanged = delegate { };

        private void Awake()
        {
            _fateService.OnAmountChanged += OnBalanceChanged;
            _buyUnitButton.OnBuyClicked += OnBuyClicked;
        }

        public void Init(int unitsInitCount, int initUnitPrice, int unitCostMultiplier,
            int maxUnitsCount)
        {
            UnitsCount = unitsInitCount;
            _unitPrice = initUnitPrice;
            MaxUnitsCount = maxUnitsCount;
            _unitCostMultiplier = unitCostMultiplier;
            OnUnitCountChanged?.Invoke(UnitsCount);
            OnMaxCountChanged?.Invoke(MaxUnitsCount);
            UpdateUi();
        }

        public void SetMaxUnitsCount(int maxUnitsCount)
        {
            MaxUnitsCount = maxUnitsCount;
            OnMaxCountChanged?.Invoke(MaxUnitsCount);
            UpdateUi();
        }

        private void UpdateUi()
        {
            _buyUnitButton.UpdateUi(
                HOUSE_BUY_UNIT_ITEM_DESC_KEY,
                $"{UnitsCount}/{MaxUnitsCount}",
                CanBuyUnit(_unitPrice, MaxUnitsCount),
                _unitPrice.ToString(CultureInfo.InvariantCulture)
            );
        }

        private void OnBalanceChanged((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            UpdateUi();
        }


        private void OnDestroy()
        {
            _fateService.OnAmountChanged -= OnBalanceChanged;
            _buyUnitButton.OnBuyClicked -= OnBuyClicked;
        }

        private void OnBuyClicked()
        {
            _fateService.Spend(_unitPrice);
            _workerService.CreateAndRegisterUnit(gameObject.transform);
            UnitsCount++;
            OnUnitCountChanged?.Invoke(UnitsCount);
            OnMaxCountChanged?.Invoke(MaxUnitsCount);
            _unitPrice = RecalculateUnitCost();
            UpdateUi();
        }

        private int RecalculateUnitCost()
        {
            return UnitsCount * _unitCostMultiplier;
        }

        private bool CanBuyUnit(float unitPrice, int maxUnitsCount)
        {
            return _fateService.HasEnough((int)unitPrice) && UnitsCount < maxUnitsCount;
        }
    }
}