using System;
using System.Globalization;
using _Project.Scripts.Localization;
using _Project.Scripts.Utils;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.Tooltips
{
    public class TooltipUi : ContentUi
    {
        [Inject] private readonly TooltipController _tooltipController;
        [Inject] private readonly UiSettings _settings;
        
        [field: SerializeField] private TooltipUiData _uiData;
        [Header("Fields")]
        [field: SerializeField] private ToLocalize _titleToLocalize;
        [field: SerializeField] private ToLocalize _levelDescToLocalize;
        [field: SerializeField] private TextMeshProUGUI _level;
        [field: SerializeField] private ToLocalize _descToLocalize;
        [field: SerializeField] private TextMeshProUGUI _priceCount;

        public RectTransform Target { get; private set; }
        private Vector3 _originalScale;
        private Tween _tween;

        public event Action OnOpened = delegate { };

        private void Awake()
        {
            _originalScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        private void OnDestroy()
        {
            _tween?.Kill();
        }

        public void SetData(TooltipUiData uiData, RectTransform target)
        {
            Target = target;
            _titleToLocalize.SetKey(uiData.TitleKey);
            _levelDescToLocalize.SetKey(uiData.LevelDescKey);
            _level.text = uiData.Level.ToString();
            _descToLocalize.SetKey(uiData.DescriptionKey);
            _priceCount.text = uiData.Price.ToString(CultureInfo.InvariantCulture);
        }

        public void AnimateAndShow()
        {
            _tooltipController.Register(this);
            Show();
            transform.localScale = _originalScale * _settings.PopupScaleStart;

            _tween?.Kill();
            _tween = transform.DOScale(_originalScale * _settings.PopupScaleOvershootStart, _settings.PopupOpenDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() => transform.DOScale(_originalScale, _settings.PopupOpenDuration / 2f));
            OnOpened?.Invoke();
        }

        public void AnimateAndHide()
        {
            _tween?.Kill();
            _tween = transform.DOScale(Vector3.one * _settings.PopupCloseScale, _settings.PopupCloseDuration)
                .SetEase(_settings.PopupCloseEase)
                .OnComplete(Hide);
        }
    }
}