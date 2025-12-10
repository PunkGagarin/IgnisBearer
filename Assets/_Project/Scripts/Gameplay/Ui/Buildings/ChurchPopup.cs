using System;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Buildings
{
    internal class ChurchPopup : ContentUi
    {
        public event Action OnUnitsAddClicked = delegate { };
        public event Action OnUnitsRemoveClicked = delegate { };

        [Inject] private WorkerService _workerService;

        [SerializeField] private AddRemoveWithCountRow _workersCountRow;

        private IWorkers _workers;
        private IGrade _grade;
        private int _maxCount;

        private void Awake()
        {
            _workersCountRow.OnAddClicked += OnAddClicked;
            _workersCountRow.OnRemoveClicked += OnRemoveClicked;
            _workers = GetComponent<Workers>();
            _grade = GetComponent<IGrade>();
            _workerService.OnWorkerListUpdated += UpdateUi;
        }

        private void Start() => UpdateUi();

        private void OnDestroy()
        {
            _workersCountRow.OnAddClicked -= OnAddClicked;
            _workersCountRow.OnRemoveClicked -= OnRemoveClicked;
            _workerService.OnWorkerListUpdated -= UpdateUi;
        }

        private void UpdateUi()
        {
            var countText = $"{_workers.CurrentCount}/{_workers.MaxCount}";
            _workersCountRow.UpdateUi(countText, CanAddUnit(), CanRemoveUnit());
        }

        private void OnRemoveClicked()
        {
            _workers.RemoveWorker(out var worker);
            _workerService.RegisterUnit(worker);
            OnUnitsRemoveClicked?.Invoke();
            UpdateUi();
        }

        private void OnAddClicked()
        {
            var unit = _workerService.UnregisterFirstFreeWorker();
            _workers.AddWorker(unit);
            OnUnitsAddClicked.Invoke();
            UpdateUi();
            //прервать текущую операцию

            // unit.StateMachine.Enter<UnitMoveToChurchState, Vector3>(transform.position);
            //направить его в наше здание
            //зарегать его в нашем здании
        }

        private bool CanRemoveUnit() => _workers.HasAnyWorker();

        private bool CanAddUnit() => _workerService.HasWorkers() && _workers.CanAddWorker();
    }
}