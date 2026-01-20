using _Project.Scripts.Gameplay.Ui;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    [RequireComponent(typeof(IResourceStorage))]
    public class FateUiController : MonoBehaviour
    {
        [Inject] private FateUi _fateUi;

        private IResourceStorage _resourceStorage;
        private FloatingMessage _floatingMessage;

        private void Awake()
        {
            _resourceStorage = GetComponent<IResourceStorage>();
            _floatingMessage = GetComponent<FloatingMessage>();
        }

        private void Start()
        {
            _resourceStorage.OnAmountIncreased += OnAmountIncreased;
            _resourceStorage.OnAmountDecreased += OnAmountDecreased;
        }

        private void OnDestroy()
        {
            _resourceStorage.OnAmountIncreased -= OnAmountIncreased;
            _resourceStorage.OnAmountDecreased -= OnAmountDecreased;
        }

        private void OnAmountIncreased((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            _floatingMessage?.Play($"+ {obj.amountIncreased}");
            UpdateFateUi(obj);
        }

        private void OnAmountDecreased((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            UpdateFateUi(obj);
        }

        private void UpdateFateUi((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            if (!_fateUi.IsShown())
                _fateUi.Show();
            _fateUi.SetFateCounter(obj.newAmount);
        }
    }
}