using _Project.Scripts.Gameplay.Ui.Buildings;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BuildingUnitIncreaser : MonoBehaviour
    {

        [Inject] private WorkerService _workerService;

        [field: SerializeField]
        public AddRemoveWithCountRow WorkersCountRow { get; private set; }


        private Workers _workers;

        private void Awake()
        {
            _workers = GetComponent<Workers>();
        }

        private void Start()
        {
            WorkersCountRow.OnAddClicked += OnAddClicked;
            WorkersCountRow.OnRemoveClicked += OnRemoveClicked;
            _workerService.OnWorkerListUpdated += UpdateUi;

            UpdateUi();
        }

        private void OnDestroy()
        {
            WorkersCountRow.OnAddClicked -= OnAddClicked;
            WorkersCountRow.OnRemoveClicked -= OnRemoveClicked;
            _workerService.OnWorkerListUpdated -= UpdateUi;
        }

        private void UpdateUi()
        {
            var countText = $"{_workers.CurrentCount}/{_workers.MaxCount}";
            WorkersCountRow.UpdateUi(countText, CanAddUnit(), CanRemoveUnit());
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