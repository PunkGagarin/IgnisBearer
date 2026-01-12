using _Project.Scripts.Gameplay.Ui;
using _Project.Scripts.Gameplay.Ui.UiEffects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class BuildingSlot : MonoBehaviour
    {
        public string Id { get; private set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _content;
        [SerializeField] private Button _button;
        [FormerlySerializedAs("popup")] [FormerlySerializedAs("_popupShower")] [SerializeField] private UiPopupDisplayer popupDisplayer;

        [Inject] private BuildingAddingOptionsService _buildingAddingOptionsService;
        [Inject] private BuildingsService _buildingsService;
        [Inject] private FateService _fateService;

        private AddBuildingPopup _addBuildingPopup;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
            _addBuildingPopup = GetComponent<AddBuildingPopup>();
            _addBuildingPopup.OnAddBuilding += OnAddBuildingClicked;
            _fateService.OnAmountChanged += OnBalanceChanged;
            SetButtonEnabled(false);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
            _addBuildingPopup.OnAddBuilding -= OnAddBuildingClicked;
            _fateService.OnAmountChanged -= OnBalanceChanged;
        }

        public void Init(string id)
        {
            Id = id;
        }

        private void OnAddBuildingClicked(BuildingType buildingType)
        {
            _addBuildingPopup.Hide();
            _buildingsService.AddBuildingTo(buildingType, this);
        }

        private void OnClick()
        {
            popupDisplayer.AnimateAndShow();
            InitPopup();
        }

        private void InitPopup()
        {
            var popupData = _buildingAddingOptionsService.GetAddBuildingPopupData();
            _addBuildingPopup.Init(popupData);
        }

        public void SetEnabled(bool isEnabled)
        {
            _content.gameObject.SetActive(isEnabled);
        }

        public void SetButtonEnabled(bool isEnabled)
        {
            _button.interactable = isEnabled;
        }

        private void OnBalanceChanged((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            InitPopup();
        }

    }
}