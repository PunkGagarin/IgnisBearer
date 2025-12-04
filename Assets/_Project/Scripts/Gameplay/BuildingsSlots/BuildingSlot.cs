using System;
using _Project.Scripts.Gameplay.Buildings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.BuildingsSlots
{
    public class BuildingSlot : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private AddBuildingPopup _addBuildingPopup;

        [Inject] private BuildingsService _buildingsService;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
            _addBuildingPopup.OnAddBuilding += OnAddBuildingClicked;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
            _addBuildingPopup.OnAddBuilding -= OnAddBuildingClicked;
        }

        private void OnAddBuildingClicked(BuildingType buildingType)
        {
            _buildingsService.AddBuildingTo(buildingType, this);
        }

        private void OnClick()
        {
            _addBuildingPopup.Show();
            var popupData = _buildingsService.GetAddBuildingPopupData();
            _addBuildingPopup.Init(popupData);
        }

        public void SetEnabled(bool enabled) => _button.gameObject.SetActive(enabled);

        public void SetSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;
    }
}