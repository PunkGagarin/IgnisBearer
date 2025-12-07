using System;
using _Project.Scripts.Gameplay.Ui;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [RequireComponent(typeof(LanternLightProducer))]
    public class LanternUi : MonoBehaviour
    {
        [Inject] private LanternSettings _settings;

        [field: SerializeField]
        public BarUi Bar { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI AmountText { get; private set; }

        [field: SerializeField]
        public GameObject Indicator { get; private set; }

        private LanternLightProducer _producer;
        private ILightStorage _lightStorage;
        private Lantern _lantern;

        public void Init()
        {
            AmountText.text = $" {_lightStorage.Amount}/{_lightStorage.MaxAmount}";
            Indicator.SetActive(true);
            Bar.TurnOffBar();
        }

        private void Awake()
        {
            _producer = GetComponent<LanternLightProducer>();
            _lightStorage = GetComponent<ILightStorage>();
            _lantern = GetComponent<Lantern>();
        }

        private void Start()
        {
            _producer.OnLightProgressed += SetProgress;
            _lightStorage.OnAmountIncreased += SetAmount;
            _lightStorage.OnStorageCleared += TurnOffIndicator;
            _lantern.OnFired += TurnOffIndicator;
        }

        private void OnDestroy()
        {
            _producer.OnLightProgressed -= SetProgress;
            _lightStorage.OnAmountIncreased -= SetAmount;
            _lightStorage.OnStorageCleared -= TurnOffIndicator;
            _lantern.OnFired -= TurnOffIndicator;
        }

        private void TurnOnIndicator()
        {
            Indicator.SetActive(true);
        }

        private void TurnOffIndicator()
        {
            Indicator.SetActive(false);
        }

        public void SetProgress(float progress)
        {
            if (!Bar.gameObject.activeSelf)
                Bar.TurnOnBar();

            Bar.ChangeBarProgress(progress);
        }

        public void SetAmount((int amountIncreased, int newAmount, int maxAmount) args)
        {
            AmountText.text = $" {args.newAmount}/{args.maxAmount}";

            if (_lightStorage.IsFull())
            {
                Bar.TurnOffBar();
                TurnOnIndicator();
            }
        }
    }
}