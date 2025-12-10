using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    internal class ChurchPopup : ContentUi
    {
        [Inject] private WorkerService _workerService;
        [Inject] private BuildingsService _buildingsService;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _addUnitButton;
        [SerializeField] private Button _removeUnitButton;
        [SerializeField] private TMP_Text _countText;

        private IWorkers _workers;
        private IGrade _grade;
        private int _maxCount;

        private void Awake()
        {
            _closeButton.onClick.AddListener(Hide);
            _addUnitButton.onClick.AddListener(OnAddClicked);
            _removeUnitButton.onClick.AddListener(OnRemoveClicked);
            _workers = GetComponent<Workers>();
            _grade = GetComponent<IGrade>();
            _workerService.OnWorkerListUpdated += UpdateUi;
        }

        private void Start()
        {
            UpdateUi();
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Hide);
            _addUnitButton.onClick.RemoveListener(OnAddClicked);
            _removeUnitButton.onClick.RemoveListener(OnRemoveClicked);
            _workerService.OnWorkerListUpdated -= UpdateUi;
        }

        private void UpdateUi()
        {
            _countText.text = $"{_workers.CurrentCount}/{_workers.MaxCount}";
            _addUnitButton.interactable = CanAddUnit();
            _removeUnitButton.interactable = CanRemoveUnit();
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
            //прервать текущую операцию

            // unit.StateMachine.Enter<UnitMoveToChurchState, Vector3>(transform.position);
            //направить его в наше здание
            //зарегать его в нашем здании
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