using _Project.Scripts.GD;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(LightConsumer))]
    [RequireComponent(typeof(IResourceStorage))]
    public class LightConsumeProgressor : MonoBehaviour
    {
        [Inject] private readonly LightConsumeSettings _lightConsumeSettings;
        [Inject] private readonly BuildingsService _buildingsService;
        [Inject] private readonly GDSettings _gdSettings;

        private IResourceStorage _lightStorage;
        private IResourceStorage _fateStorage;
        private LightConsumer _lightConsumer;

        private int _nextProgressIndex = 0;

        private float _currentProgressTime;
        private LightConsumeProgress _nextProgress;

        private void Awake()
        {
            _lightStorage = GetComponent<IResourceStorage>();
            _lightConsumer = GetComponent<LightConsumer>();
        }

        private void Start()
        {
            _fateStorage = _buildingsService.GetFateStorage();
            _nextProgress = _lightConsumeSettings.GetProgressByIndex(_nextProgressIndex);
            if (_gdSettings.IsConsumeStartedByDefault)
            {
                StartConsumeLight();
            }
            else
            {
                _lightStorage.OnAmountIncreased += StartConsumeLightHandle;
                _fateStorage.OnAmountIncreased += StartConsumeLightHandle;
            }
        }

        private void StartConsumeLightHandle((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            StartConsumeLight();
        }

        private void StartConsumeLight()
        {
            _lightConsumer.IsConsumeStarted = true;

            SetNextProgress();

            if (!_gdSettings.IsConsumeStartedByDefault)
            {
                _lightStorage.OnAmountIncreased -= StartConsumeLightHandle;
                _fateStorage.OnAmountIncreased -= StartConsumeLightHandle;
            }
        }

        private void SetNextProgress()
        {
            _lightConsumer.Init(_nextProgress.TimeToConsume, _nextProgress.Amount);
            _nextProgress = GetProgressWithIncrement();
            Debug.Log(
                $"Light consume Progress set at {_currentProgressTime}, next progress at: {_nextProgress.TimeToIncrease}");
        }

        private LightConsumeProgress GetProgressWithIncrement()
        {
            var progress = _lightConsumeSettings.GetProgressByIndex(_nextProgressIndex);
            _nextProgressIndex++;
            return progress;
        }

        public void Update()
        {
            if (!_lightConsumer.IsConsumeStarted)
                return;

            _currentProgressTime += Time.deltaTime;
            if (_currentProgressTime >= _nextProgress.TimeToIncrease)
            {
                SetNextProgress();
            }
        }
    }

}