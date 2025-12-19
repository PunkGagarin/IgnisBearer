using _Project.Scripts.Gameplay.Ui;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.FateGenerator
{
    public class FateUiController : MonoBehaviour
    {
        [Inject] private FateUi _fateUi;

        private IResourceStorage _resourceStorage;

        private void Awake()
        {
            _resourceStorage = GetComponent<IResourceStorage>();
        }

        private void Start()
        {
            _resourceStorage.OnAmountIncreased += UpdateFateUi;
            _resourceStorage.OnAmountDecreased += UpdateFateUi;
        }

        private void OnDestroy()
        {
            _resourceStorage.OnAmountIncreased -= UpdateFateUi;
            _resourceStorage.OnAmountDecreased -= UpdateFateUi;
        }

        private void UpdateFateUi((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            if (!_fateUi.IsShown())
                _fateUi.Show();
            _fateUi.SetFateCounter(obj.newAmount);
        }
    }
}