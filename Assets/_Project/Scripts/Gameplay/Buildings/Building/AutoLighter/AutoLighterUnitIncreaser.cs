using System;
using _Project.Scripts.Gameplay.Buildings;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Buildings
{
    public class AutoLighterUnitIncreaser : ContentUi
    {

        [Inject] private WorkerService _workerService;

        [field: SerializeField]
        public AutoLighterPopup AutoLighterPopup { get; private set; }


        private Workers _workers;
        private IGrade _grade;

        private void Awake()
        {
            AutoLighterPopup.WorkersCountRow.OnAddClicked += OnAddClicked;
            AutoLighterPopup.WorkersCountRow.OnRemoveClicked += OnRemoveClicked;
            _workers = GetComponent<Workers>();
            _grade = GetComponent<IGrade>();
            _workerService.OnWorkerListUpdated += UpdateUi;
        }

        private void Start() => UpdateUi();

        private void OnDestroy()
        {
            AutoLighterPopup.WorkersCountRow.OnAddClicked -= OnAddClicked;
            AutoLighterPopup.WorkersCountRow.OnRemoveClicked -= OnRemoveClicked;
            _workerService.OnWorkerListUpdated -= UpdateUi;
        }

        private void UpdateUi()
        {
            var countText = $"{_workers.CurrentCount}/{_workers.MaxCount}";
            AutoLighterPopup.WorkersCountRow.UpdateUi(countText, CanAddUnit(), CanRemoveUnit());
        }

        private void OnRemoveClicked()
        {
            _workers.RemoveWorker(out var worker);
            _workerService.RegisterUnit(worker);
            UpdateUi();
        }

        private void OnAddClicked()
        {
            var unit = _workerService.UnregisterFirstFreeWorker();
            _workers.AddWorker(unit);

            UpdateUi();
            unit.StateMachine.Enter<UnitMoveToState, Vector3>(transform.position);
        }

        private bool CanRemoveUnit()
        {
            return _workers.HasAnyWorker();
        }

        private bool CanAddUnit()
        {
            return _workerService.HasWorkers() && _workers.CanAddWorker();
        }
    }

}