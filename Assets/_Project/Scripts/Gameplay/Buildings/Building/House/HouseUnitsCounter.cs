using _Project.Scripts.Gameplay.Ui.Buildings;
using _Project.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class HouseUnitsCounter : ContentUi
    {
        [Inject] private BuildingsService _buildingsService;
        [SerializeField] private UnitsCountView _unitsCountView;

        private HouseBuyUnit _houseBuyUnit;

        private void Awake()
        {
            _buildingsService.OnHouseBuilt += Init;
        }

        private void Start()
        {
            Hide();
        }

        private void Init(HouseBuilding building)
        {
            _houseBuyUnit = building.GetComponent<HouseBuyUnit>();
            _houseBuyUnit.OnUnitCountChanged += UpdateUi;
            _houseBuyUnit.OnMaxCountChanged += UpdateUi;
            Show();
            UpdateUi();
        }

        private void UpdateUi(int obj) => UpdateUi();

        private void OnDestroy()
        {
            if (_houseBuyUnit != null)
            {
                _houseBuyUnit.OnUnitCountChanged -= UpdateUi;
                _houseBuyUnit.OnMaxCountChanged -= UpdateUi;
            }
            _buildingsService.OnHouseBuilt -= Init;
        }

        private void UpdateUi()
        {
            var curCount = _houseBuyUnit.UnitsCount;
            var maxCount = _houseBuyUnit.MaxUnitsCount;
            _unitsCountView.Init(curCount, maxCount);
        }
    }
}