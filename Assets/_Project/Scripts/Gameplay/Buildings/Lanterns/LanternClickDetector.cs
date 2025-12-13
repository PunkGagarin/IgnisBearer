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
        private ResourceStorage _resourceStorage;

        private void Awake()
        {
            _lantern = GetComponent<Lantern>();
            _resourceStorage = GetComponent<ResourceStorage>();

            Button.onClick.AddListener(OnButtonClicked);
            _resourceStorage.OnAmountIncreased += OnAmountIncreaseHandle;
            _resourceStorage.OnStorageCleared += TurnOffClick;
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveListener(OnButtonClicked);
            _resourceStorage.OnAmountIncreased -= OnAmountIncreaseHandle;
            _resourceStorage.OnStorageCleared -= TurnOffClick;
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