using System;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Buildings
{
    internal class HousePopup : ContentUi
    {
        public event Action<float> OnBuyClicked = delegate { };

        // [Inject] private GoldService _goldService;
        [Inject] private WorkerService _workerService;
        [Inject] private BuildingsService _buildingsService;

        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _priceText;

        private IGrade _grade;

        private void Awake()
        {
            _buyButton.onClick.AddListener(OnBuyUnitClicked);
            _grade = GetComponent<IGrade>();
            // _goldService.OnBalanceChanged += OnBalanceChanged;
        }

        private void Start()
        {
            var curGrade = _grade.Current;
            var unitPrice = _buildingsService.GetHouseUnitPrice(curGrade);
            _priceText.text = $"{unitPrice}";
            UpdateBuyButton(unitPrice);
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnBuyUnitClicked);
            // _goldService.OnBalanceChanged -= OnBalanceChanged;
        }

        private void OnBuyUnitClicked()
        {
            // _goldService.TakeGold(cost);
            _workerService.CreateAndRegisterUnit(gameObject.transform);
        }

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