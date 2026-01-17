using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay.Ui.UiEffects
{
    [RequireComponent(typeof(Button), typeof(UiShake))]
    public class UiButtonClickEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Inject] private readonly UiSettings _settings;

        private Vector3 _originalScale;
        private UiShake _shake;
        protected Button _button;

        private void Awake()
        {
            _shake = GetComponent<UiShake>();
            _button = GetComponent<Button>();
            _originalScale = transform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!CanClick())
            {
                _shake.Shake();
                return;
            }
            ClickDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!CanClick()) return;
            ClickUp();
        }

        protected virtual  void ClickDown()
        {
            transform.DOScale(_originalScale * _settings.ClickPressedScale, _settings.ClickScaleDuration).SetEase(Ease.OutQuad);
        }

        protected virtual void ClickUp()
        {
            transform.DOScale(_originalScale, _settings.ClickScaleDuration).SetEase(Ease.OutQuad);
        }

        private bool CanClick()
        {
            return _button.interactable;
        }
    }
}