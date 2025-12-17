using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.BuildingsSlots
{
    public class BuildingSlot : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _content;

        [Inject] private BuildingsService _buildingsService;
        [Inject] private FateService _fateService;
        [Inject] private BuildingAddingOptionsService _buildingAddingOptionsService;

        private AddBuildingPopup _addBuildingPopup;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
            _addBuildingPopup = GetComponent<AddBuildingPopup>();
            _addBuildingPopup.OnAddBuilding += OnAddBuildingClicked;
            _fateService.OnAmountChanged += OnBalanceChanged;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
            _addBuildingPopup.OnAddBuilding -= OnAddBuildingClicked;
            _fateService.OnAmountChanged -= OnBalanceChanged;
        }

        private void OnAddBuildingClicked(BuildingType buildingType)
        {
            _addBuildingPopup.Hide();
            _buildingsService.AddBuildingTo(buildingType, this);
        }

        private void OnClick()
        {
            _addBuildingPopup.Show();
            InitPopup();
        }

        private void InitPopup()
        {
            var popupData = _buildingAddingOptionsService.GetAddBuildingPopupData();
            _addBuildingPopup.Init(popupData);
        }

        public void SetEnabled(bool enabled) => _content.gameObject.SetActive(enabled);

        private void OnBalanceChanged((int amountIncreased, int newAmount, int maxAmount) obj) => InitPopup();
    }
}