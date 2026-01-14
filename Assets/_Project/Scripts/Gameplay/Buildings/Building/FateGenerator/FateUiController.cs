using System;
using System.Threading.Tasks;
using _Project.Scripts.Gameplay.Ui;
using Cysharp.Threading.Tasks;
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
            _floatingMessage?.Play($"+ {obj.amountIncreased}");
            if (!_fateUi.IsShown())
                _fateUi.Show();
            _fateUi.SetFateCounter(obj.newAmount);
        }
    }
}
