using System;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Buildings
{
    public class AutoLighterUi : ContentUi
    {
        [Inject] private WorkerService _workerService;

        [field: SerializeField]
        public TextMeshProUGUI UnitAmountText { get; private set; }

        [field: SerializeField]
        public Button AddUnitButton { get; private set; }

        private Workers _workers;

        public event Action OnUnitsAddClicked = delegate { };

        private void Awake()
        {
            _workers = GetComponent<Workers>();
        }

        private void Start()
        {
            AddUnitButton.onClick.AddListener(OnUnitsAddClickedHandle);
        }

        private void OnDestroy()
        {
            AddUnitButton.onClick.RemoveListener(OnUnitsAddClickedHandle);
        }

        private void OnUnitsAddClickedHandle()
        {
            OnUnitsAddClicked.Invoke();
        }

        private void TryAddUnit()
        {
            if (_workerService.HasWorkers() && _workers.CanAddWorker())
            {
                var unit = _workerService.UnregisterFirstFreeWorker();
                _workers.AddWorker(unit);

                //прервать текущую операцию

                // unit.StateMachine.Enter<UnitMoveToChurchState, Vector3>(transform.position);
                //направить его в наше здание
                //зарегать его в нашем здании
            }
        }
    }
}