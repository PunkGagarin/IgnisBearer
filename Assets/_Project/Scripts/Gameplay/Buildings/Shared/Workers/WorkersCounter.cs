using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(IWorkers))]
    public class WorkersCounter : MonoBehaviour
    {
        [SerializeField] private UnitsCountView _unitsCountView;

        private IWorkers _workers;

        private void Awake()
        {
            _workers = GetComponent<IWorkers>();
            _workers.OnUnitAdded += UpdateUi;
            _workers.OnUnitRemoved += UpdateUi;
        }
        
        private void Start()
        {
            UpdateUi();
        }
        
        private void UpdateUi(Unit obj) => UpdateUi();

        private void OnDestroy()
        {
            _workers.OnUnitAdded -= UpdateUi;
            _workers.OnUnitRemoved -= UpdateUi;
        }

        private void UpdateUi()
        {
            var curWorkersCount = _workers.CurrentCount;
            var maxWorkersCount = _workers.MaxCount;
            _unitsCountView.Init(curWorkersCount, maxWorkersCount);
        }
    }
}