using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LanternClickDetector : MonoBehaviour
    {

        [field: SerializeField]
        public Button Button { get; private set; }

        public Action<Lantern> OnClicked = delegate { };

        private Lantern _lantern;
        private LightStorage _lightStorage;

        private void Awake()
        {
            _lantern = GetComponent<Lantern>();
            _lightStorage = GetComponent<LightStorage>();

            Button.onClick.AddListener(OnButtonClicked);
            _lightStorage.OnAmountIncreased += OnAmountIncreaseHandle;
            _lightStorage.OnStorageCleared += TurnOffClick;
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveListener(OnButtonClicked);
            _lightStorage.OnAmountIncreased -= OnAmountIncreaseHandle;
            _lightStorage.OnStorageCleared -= TurnOffClick;
        }

        private void OnButtonClicked()
        {
            OnClicked.Invoke(_lantern);
        }

        private void OnAmountIncreaseHandle((int amountIncreased, int newAmount, int maxAmount) _)
        {
            TurnOnClick();
        }

        public void TurnOnClick()
        {
            Button.gameObject.SetActive(true);
        }

        public void TurnOffClick()
        {
            Button.gameObject.SetActive(false);
        }
    }
}