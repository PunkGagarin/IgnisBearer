using _Project.Scripts.Gameplay.Ui;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [RequireComponent(typeof(ResourceProducer))]
    public class LanternUi : MonoBehaviour
    {
        [Inject] private LanternSettings _settings;

        [field: SerializeField]
        public BarUi Bar { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI AmountText { get; private set; }

        [field: SerializeField]
        public GameObject Indicator { get; private set; }

        private ResourceProducer _producer;
        private IResourceStorage _iResourceStorage;
        private Lantern _lantern;

        public void Init()
        {
            SetCurrentAmountText();
            Indicator.SetActive(true);
            Bar.TurnOffBar();
        }

        private void Awake()
        {
            _producer = GetComponent<ResourceProducer>();
            _iResourceStorage = GetComponent<IResourceStorage>();
            _lantern = GetComponent<Lantern>();
        }

        private void Start()
        {
            _producer.OnLightProgressed += SetProgress;
            _iResourceStorage.OnAmountIncreased += SetAmount;
            _iResourceStorage.OnStorageCleared += ClearLantern;
            _iResourceStorage.OnStartHarvest += TurnOffIndicator;
            _lantern.OnFired += TurnOffIndicator;
            _lantern.OnFireOff += TurnOffBar;
        }

        private void OnDestroy()
        {
            _producer.OnLightProgressed -= SetProgress;
            _iResourceStorage.OnAmountIncreased -= SetAmount;
            _iResourceStorage.OnStorageCleared -= ClearLantern;
            _iResourceStorage.OnStartHarvest -= TurnOffIndicator;
            _lantern.OnFired -= TurnOffIndicator;
            _lantern.OnFireOff -= TurnOffBar;
        }

        private void TurnOffBar()
        {
            Bar.TurnOffBar();
        }

        private void SetCurrentAmountText()
        {
            AmountText.text = $" {_iResourceStorage.Amount}/{_iResourceStorage.MaxAmount}";
        }

        private void ClearLantern()
        {
            TurnOffIndicator();
            SetCurrentAmountText();
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
            SetCurrentAmountText();

            if (_iResourceStorage.IsFull())
            {
                TurnOffBar();
                TurnOnIndicator();
            }
        }
    }
}