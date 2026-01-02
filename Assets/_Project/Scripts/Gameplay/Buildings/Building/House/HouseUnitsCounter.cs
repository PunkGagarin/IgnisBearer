using _Project.Scripts.Gameplay.Ui.Buildings;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(HouseBuyUnit))]
    public class HouseUnitsCounter : MonoBehaviour
    {
        [SerializeField] private UnitsCountView _unitsCountView;

        private HouseBuyUnit _houseBuyUnit;

        private void Awake()
        {
            _houseBuyUnit = GetComponent<HouseBuyUnit>();
            _houseBuyUnit.OnUnitCountChanged += UpdateUi;
            _houseBuyUnit.OnMaxCountChanged += UpdateUi;
        }

        private void Start()
        {
            UpdateUi();
        }

        private void UpdateUi(int obj) => UpdateUi();

        private void OnDestroy()
        {
            _houseBuyUnit.OnUnitCountChanged -= UpdateUi;
            _houseBuyUnit.OnMaxCountChanged -= UpdateUi;
        }

        private void UpdateUi()
        {
            var curCount = _houseBuyUnit.UnitsCount;
            var maxCount = _houseBuyUnit.MaxUnitsCount;
            _unitsCountView.Init(curCount, maxCount);
        }
    }
}