using System.Globalization;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
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

        private int _unitsCount;
        private int _unitPrice;
        private int _maxUnitsCount;

        private void Awake()
        {
            _fateService.OnAmountChanged += OnBalanceChanged;
            _buyUnitButton.OnBuyClicked += OnBuyClicked;
        }

        public void Init(int unitsInitCount, int unitPrice, int maxUnitsCount)
        {
            _unitsCount = unitsInitCount;
            _unitPrice = unitPrice;
            _maxUnitsCount = maxUnitsCount;
            UpdateUi();
        }

        public void SetMaxUnitsCount(int maxUnitsCount)
        {
            _maxUnitsCount = maxUnitsCount;
            UpdateUi();
        }

        public void SetUnitPrice(int unitPrice)
        {
            _unitPrice = unitPrice;
            UpdateUi();
        }

        private void UpdateUi()
        {
            _buyUnitButton.UpdateUi(
                HOUSE_BUY_UNIT_ITEM_DESC_KEY,
                $"{_unitsCount}/{_maxUnitsCount}",
                CanBuyUnit(_unitPrice, _maxUnitsCount),
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
            _unitsCount++;
            UpdateUi();
        }

        private bool CanBuyUnit(float unitPrice, int maxUnitsCount)
        {
            return _fateService.HasEnough((int)unitPrice) && _unitsCount < maxUnitsCount;
        }
    }
}