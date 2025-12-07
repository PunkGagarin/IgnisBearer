using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class HouseBuilding : Building
    {
        [Inject] private WorkerService _workerService;
        [Inject] private BuildingsService _buildingsService;
        // [Inject] private GoldService _goldService;

        [SerializeField] private BuyUnitsPopup _buyUnitsPopup;

        private IDurability _durability;

        protected override void Awake()
        {
            base.Awake();
            _durability = GetComponent<IDurability>();
            _durability.OnDestroyed += OnBuildingBroke;
            _buyUnitsPopup.OnBuyClicked += OnBuyClicked;
        }

        private void OnDestroy()
        {
            if (_durability != null)
                _durability.OnDestroyed -= OnBuildingBroke;
            _buyUnitsPopup.OnBuyClicked -= OnBuyClicked;
        }

        private void OnBuyClicked(float cost)
        {
            // _goldService.TakeGold(cost);
            _buyUnitsPopup.Hide();
            _workerService.CreateAndRegisterUnit(gameObject.transform);
        }

        private void OnBuildingBroke()
        {
            _durability.OnDestroyed -= OnBuildingBroke;
            Destroy(gameObject);
        }

        protected override void HandleButtonClick()
        {
            _buyUnitsPopup.Show();
            _buyUnitsPopup.Init(_buildingsService.GetUnitPurchaseData());
        }
    }
}