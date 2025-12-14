using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(LightConsumer))]
    [RequireComponent(typeof(IResourceStorage))]
    public class LightConsumeProgressor : MonoBehaviour
    {
        [Inject] private readonly LightConsumeSettings _lightConsumeSettings;

        private IResourceStorage _lightStorage;
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
            _lightStorage.OnAmountIncreased += StartConsumeLight;

            _nextProgress = _lightConsumeSettings.GetProgressByIndex(_nextProgressIndex);
        }

        private void StartConsumeLight((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            _lightConsumer.IsConsumeStarted = true;

            SetNextProgress();

            _lightStorage.OnAmountIncreased -= StartConsumeLight;
        }

        private void SetNextProgress()
        {
            Debug.Log("Setting next progress");
            _lightConsumer.Init(_nextProgress.TimeToConsume, _nextProgress.Amount);
            _nextProgress = GetProgressWithIncrement();
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