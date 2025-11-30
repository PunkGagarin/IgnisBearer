using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.BuildingsSlots;
using _Project.Scripts.Gameplay.House;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts
{
    public class TestDebug : MonoBehaviour
    {
        [SerializeField] private Button _addUnitButton;
        [SerializeField] private BuildingSlot _buildingSlot;
        
        [Inject] private BuildingFactory _buildingFactory;
        [Inject] private UnitFactory _unitFactory;

        private void Awake()
        {
            _addUnitButton.onClick.AddListener(AddUnit);
        }

        private void Start()
        {
            var house = _buildingFactory.BuildHouse(_buildingSlot);
            house.OnHouseClicked += OnHouseClicked;
            house.OnHouseDestroyed += Unsubscribe;
        }

        private void Unsubscribe(HouseBuilding house)
        {
            house.OnHouseClicked -= OnHouseClicked;
            house.OnHouseDestroyed -= Unsubscribe;
        }

        private void OnHouseClicked(HouseBuilding obj) => AddUnit();

        private void AddUnit() => _unitFactory.CreateAndInstantiateUnit();

        private void OnDestroy() => _addUnitButton.onClick.RemoveListener(AddUnit);
    }
}