using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class GradeUpdateNotification : MonoBehaviour
    {
        [Inject] private readonly FateService _fateService;

        [field: SerializeField] private RectTransform _notification;
        [field: SerializeField] private bool _isEnabled = true;

        private IGrade _grade;

        private void Awake()
        {
            _grade = GetComponent<Grade>();
            _fateService.OnAmountChanged += OnFateBalanceChanged;
        }

        public void Init()
        {
            UpdateUi();
        }

        private void OnFateBalanceChanged((int amountIncreased, int newAmount, int maxAmount) obj)
        {
            UpdateUi();
        }

        private void UpdateUi()
        {
            _notification.gameObject.SetActive(_isEnabled && _grade.CanUpdate());
        }
    }
}