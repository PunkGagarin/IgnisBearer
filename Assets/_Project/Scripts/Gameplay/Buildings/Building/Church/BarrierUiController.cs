using _Project.Scripts.Gameplay.Ui;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BarrierUiController : MonoBehaviour
    {
        [Inject] BarrierUi _barrierUi;

        private IResourceStorage _resourceStorage;

        private void Awake()
        {
            _resourceStorage = GetComponent<IResourceStorage>();
        }

        private void Start()
        {
            _resourceStorage.OnAmountIncreased += UpdateUi;
            _resourceStorage.OnAmountDecreased += UpdateUi;
        }

        private void OnDestroy()
        {
            _resourceStorage.OnAmountIncreased -= UpdateUi;
            _resourceStorage.OnAmountDecreased -= UpdateUi;
        }

        private void UpdateUi((int amountDiff, int newAmount, int maxAmount) valueTuple)
        {
            if (!_barrierUi.IsShown())
                _barrierUi.Show();

            _barrierUi.Bar.ChangeBarProgress(valueTuple.newAmount / (float)valueTuple.maxAmount);
            _barrierUi.SetBarrierCounter(valueTuple.newAmount, valueTuple.maxAmount);
        }
    }
}