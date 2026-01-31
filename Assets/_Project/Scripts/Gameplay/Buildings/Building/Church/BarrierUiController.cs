using _Project.Scripts.Gameplay.Ui;
using _Project.Scripts.Shaders;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class BarrierUiController : MonoBehaviour
    {
        [Inject] BarrierUi _barrierUi;
        [Inject] VignetteShaderController _vignetteShaderController;

        private IResourceStorage _resourceStorage;
        private ChurchCrackController _churchCrackController;

        private void Awake()
        {
            _churchCrackController = GetComponent<ChurchCrackController>();
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
            _vignetteShaderController.SetBarrierValue(valueTuple.newAmount);
            _churchCrackController.SetBarrierValue(valueTuple.newAmount);
        }
    }
}