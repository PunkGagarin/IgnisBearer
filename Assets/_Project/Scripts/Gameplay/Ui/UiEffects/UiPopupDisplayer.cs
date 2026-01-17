using System;
using _Project.Scripts.Gameplay.Buildings.BuildingsSlots;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.UiEffects
{
    public class UiPopupDisplayer : MonoBehaviour
    {
        [Inject] private readonly UiSettings _settings;
        [Inject] private PopupController _popupController;

        private Vector3 _originalScale;
        
        public event Action OnOpened = delegate { };
        public event Action OnClosed = delegate { };

        private void Awake()
        {
            _originalScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        public void AnimateAndShow()
        {
            _popupController.Register(this);
            gameObject.SetActive(true);
            transform.localScale = _originalScale * _settings.PopupScaleStart;
            transform.DOScale(_originalScale * _settings.PopupScaleOvershootStart, _settings.PopupOpenDuration)
                .SetEase(Ease.OutBack)
                .SetUpdate(true)
                .OnComplete(() => transform.DOScale(_originalScale, _settings.PopupOpenDuration / 2f));
            OnOpened?.Invoke();
        }

        public void AnimateAndHide()
        {
            _popupController.Close(this);

            transform.DOScale(Vector3.one * _settings.PopupCloseScale, _settings.PopupCloseDuration)
                .SetEase(_settings.PopupCloseEase)
                .SetUpdate(true)
                .OnComplete(() => gameObject.SetActive(false));
            OnClosed?.Invoke();
        }
    }
}