using _Project.Scripts.Utils;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Ui
{
    public class FateUi : ContentUi
    {
        [field: SerializeField]
        public TextMeshProUGUI FateCounter { get; private set; }
        
        private Tween _counterTween;
        private float _currentValue;

        public void SetFateCounter(int target)
        {
            _counterTween?.Kill();

            _counterTween = DOTween.To(() => _currentValue, x =>
            {
                _currentValue = x;
                FateCounter.text = _currentValue.ToString("N0");
            }, target, 1f).SetEase(Ease.OutQuart);
        }
    }
}