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

        private void Awake()
        {
            _producer = GetComponent<LanternLightProducer>();
            _lightStorage = GetComponent<ILightStorage>();

            _producer.OnLightProgressed += SetProgress;
            _lightStorage.OnAmountIncreased += SetAmount;
        }

        public void Init()
        {
            AmountText.text = $" {_lightStorage.Amount}/{_lightStorage.MaxAmount}";
        }

        private void OnDestroy()
        {
            _producer.OnLightProgressed -= SetProgress;
            _lightStorage.OnAmountIncreased -= SetAmount;
        }

        public void SetProgress(float progress)
            => Bar.ChangeBarProgress(progress);

        public void SetAmount((int amountIncreased, int newAmount, int maxAmount) args)
        {
            AmountText.text = $" {args.newAmount}/{args.maxAmount}";
        }
    }
}