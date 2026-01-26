using System;
using _Project.Scripts.Gameplay.Ui.Buildings;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class Grade : MonoBehaviour, IGrade
    {
        private const string GRADE_DESC_KEY = "GRADE_UPGRADE_TITLE";

        [Inject] public FateService _fateService;

        [SerializeField] private BuyLimitedButton _buyLimitedButton;
        public int NextGradePrice { get; set; }
        public int Current { get; set; }
        public int Max { get; set; }

        public event Action<int> OnGradeChanged;

        private void Awake()
        {
            _fateService.OnAmountChanged += OnFateAmountChanged;
            _buyLimitedButton.OnBuyClicked += UpgradeGrade;
            ShowUi(false);
        }

        private void ShowUi(bool show)
        {
            _buyLimitedButton.gameObject.SetActive(show);
        }

        private void OnFateAmountChanged((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            ShowUi(true);
            UpdateUi();
        }

        private void OnDestroy() => _buyLimitedButton.OnBuyClicked -= UpgradeGrade;

        public void UpgradeGrade()
        {
            if (!CanUpdate())
            {
                Debug.LogError($"Can't update grade {Current}");
                UpdateUi();
                return;
            }

            Current++;
            _fateService.Spend(NextGradePrice);
            OnGradeChanged?.Invoke(Current);
            UpdateUi();
        }

        public void SetNextGradePrice(int nextGradePrice)
        {
            NextGradePrice = nextGradePrice;
            UpdateUi();
        }

        public void ShowGradeMaxed() => _buyLimitedButton.ShowButton();

        public void Init(int initValue, int maxGrade, int gradePrice)
        {
            Current = initValue;
            NextGradePrice = gradePrice;
            Max = maxGrade;
            UpdateUi();
            if (CanUpdate())
                ShowUi(true);
        }

        private void UpdateUi()
        {
            var countText = $"{Current}/{Max}";
            var isButtonEnabled = CanUpdate();
            _buyLimitedButton.UpdateUi(GRADE_DESC_KEY, countText, isButtonEnabled, NextGradePrice.ToString(),
                Current == Max);
        }

        public bool CanUpdate() =>
            _fateService.HasEnough(NextGradePrice) && Current < Max;
    }
}