using _Project.Scripts.Gameplay.Buildings.BuildingSlots;
using _Project.Scripts.Gameplay.Ui.UiEffects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class BuildingSlot : MonoBehaviour
    {
        public bool IsTaken { get; private set; }

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
            _fateService.OnAmountChanged += OnBalanceChanged;
            _addBuildingPopup.OnAddBuilding += OnAddBuildingClicked;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
            _addBuildingPopup.OnAddBuilding -= OnAddBuildingClicked;
            _fateService.OnAmountChanged -= OnBalanceChanged;
        }

        private void OnAddBuildingClicked(BuildingType buildingType)
        {
            IsTaken = true;
            _buildingsService.AddBuildingTo(buildingType, this);
        }

        private void OnClick()
        {
            popupDisplayer.AnimateAndShow();
            SetPopupData();
        }

        private void SetPopupData()
        {
            var popupData = _buildingAddingOptionsService.GetAddBuildingPopupData();
            _addBuildingPopup.SetData(popupData);
        }

        public void SetEnabled(bool isEnabled)
        {
            _content.gameObject.SetActive(isEnabled);
        }

        public void SetButtonInteractable(bool isEnabled)
        {
            _button.GetComponent<UiButtonEnableEffect>().SetInteractable(isEnabled);
        }

        private void OnBalanceChanged((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            SetPopupData();
        }
    }
}