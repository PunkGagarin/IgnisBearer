using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.UiEffects
{
    [RequireComponent(typeof(RectTransform))]
    public class UiHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
       [Inject] private readonly UiSettings _settings;
        
        private Vector3 _originalScale;
        private Tween _hoverTween;

        protected virtual void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!CanScale()) return;
            _hoverTween?.Kill();
            _hoverTween = transform.DOScale(_originalScale * _settings.HoverScale, _settings.HoverDuration).SetEase(Ease.OutBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!CanScale()) return;
            _hoverTween?.Kill();
            _hoverTween = transform.DOScale(_originalScale, _settings.HoverDuration).SetEase(Ease.OutBack);
        }

        protected virtual bool CanScale()
        {
            return true;
        }
    }
}