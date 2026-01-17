using _Project.Scripts.Gameplay.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [RequireComponent(typeof(ResourceProducer))]
    [RequireComponent(typeof(LanternClickDetector))]
    public class LanternUi : MonoBehaviour
    {
        [Inject] private LanternSettings _settings;

        [field: SerializeField]
        public BarUi Bar { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI AmountText { get; private set; }

        [field: SerializeField]
        public Light2D Light { get; private set; }

        [field: SerializeField]
        public GameObject Indicator { get; private set; }

        private ResourceProducer _producer;
        // private IResourceStorage _iResourceStorage;
        private Lantern _lantern;
        private LanternClickDetector _clickDetector;

        public void Init()
        {
            // SetCurrentAmountText();
            TurnOnIndicator();
            Bar.TurnOffBar();
        }

        private void Awake()
        {
            _producer = GetComponent<ResourceProducer>();
            _clickDetector = GetComponent<LanternClickDetector>();
            // _iResourceStorage = GetComponent<IResourceStorage>();
            _lantern = GetComponent<Lantern>();
        }

        private void Start()
        {
            _producer.OnLightProgressed += SetProgress;
            // _iResourceStorage.OnAmountIncreased += SetAmount;
            // _iResourceStorage.OnStorageCleared += ClearLantern;
            // _iResourceStorage.OnStartHarvest += TurnOffIndicator;
            _lantern.OnFired += TurnOffIndicator;
            _lantern.OnFired += TurnOnLight;
            _lantern.OnFired += OnLanternFired;
            _lantern.OnFireOff += TurnOffBar;
            _lantern.OnFireOff += TurnOffLight;
            _lantern.OnFireOff += TurnOnIndicator;
        }

        private void OnDestroy()
        {
            _producer.OnLightProgressed -= SetProgress;
            // _iResourceStorage.OnAmountIncreased -= SetAmount;
            // _iResourceStorage.OnStorageCleared -= ClearLantern;
            // _iResourceStorage.OnStartHarvest -= TurnOffIndicator;
            _lantern.OnFired -= TurnOffIndicator;
            _lantern.OnFired -= TurnOnLight;
            _lantern.OnFired -= OnLanternFired;
            _lantern.OnFireOff -= TurnOffBar;
            _lantern.OnFireOff -= TurnOffLight;
            _lantern.OnFireOff -= TurnOnIndicator;
        }

        private void TurnOnLight()
        {
            Light.gameObject.SetActive(true);
        }
        
        private void TurnOffLight()
        {
            Light.gameObject.SetActive(false);
        }

        private void TurnOffBar()
        {
            Bar.TurnOffBar();
        }

        private void SetCurrentAmountText()
        {
            // AmountText.text = $" {_iResourceStorage.Amount}/{_iResourceStorage.MaxAmount}";
        }

        private void ClearLantern()
        {
            if (!_lantern.IsFired())
                return;
            TurnOffIndicator();
            SetCurrentAmountText();
        }

        private void TurnOnIndicator()
        {
            Indicator.SetActive(true);
            _clickDetector.TurnOnClick();
        }

        public void OnLanternFired(Lantern _)
        {
            TurnOffIndicator();
        }

        public void TurnOffIndicator()
        {
            Indicator.SetActive(false);
            _clickDetector.TurnOffClick();
        }

        public void SetProgress(float progress)
        {
            if (!Bar.gameObject.activeSelf)
                Bar.TurnOnBar();

            Bar.ChangeBarProgress(progress);
        }

        public void SetAmount((int amountIncreased, int newAmount, int maxAmount) args)
        {
            // SetCurrentAmountText();

            // if (_iResourceStorage.IsFull())
            // {
            //     TurnOffBar();
            //     TurnOnIndicator();
            // }
        }
    }
}