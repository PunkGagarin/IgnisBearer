using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.BuildingsSlots;
using _Project.Scripts.Gameplay.Church;
using _Project.Scripts.Gameplay.House;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Gameplay.Units.Manager;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class TestDebug : MonoBehaviour
    {
        [SerializeField] private BuildingSlot _buildingSlot;

        [Inject] private BuildingFactory _buildingFactory;

        private void Awake()
        {
            _buildingSlot.OnClicked += AddHouse;
        }

        private void AddChurch(BuildingSlot obj)
        {
            var church = _buildingFactory.BuildChurch(obj);
            church.OnChurchClicked += OnChurchClicked;
            church.OnChurchDestroyed += Unsubscribe;
        }

        private void Unsubscribe(ChurchBuilding obj)
        {
            obj.OnChurchClicked -= OnChurchClicked;
            obj.OnChurchDestroyed -= Unsubscribe;
        }

        private void OnChurchClicked(ChurchBuilding church)
        {
            // idk :)
        }

        private void AddHouse(BuildingSlot obj)
        {
            var house = _buildingFactory.BuildHouse(obj);
            house.OnHouseClicked += OnHouseClicked;
            house.OnHouseDestroyed += Unsubscribe;
        }

        private void AddUnit()
        {
        }

        private void Unsubscribe(HouseBuilding house)
        {
            house.OnHouseClicked -= OnHouseClicked;
            house.OnHouseDestroyed -= Unsubscribe;
        }
        
        private void OnHouseClicked(HouseBuilding obj) => AddUnit();

        private void OnDestroy()
        {
            _buildingSlot.OnClicked -= AddHouse;
        }
    }
}