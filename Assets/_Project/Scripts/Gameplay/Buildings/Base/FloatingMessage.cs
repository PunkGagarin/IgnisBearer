using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class FloatingMessage : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _moveUpDistance;
        [SerializeField] private float _duration;
        [SerializeField] private float _downMargin = 2f;
        [SerializeField] private AnimationCurve moveCurve;

        private Sequence _sequence;
        private Vector2 _startPos;

        private void Start()
        {
            _startPos = _rectTransform.anchoredPosition;
            Hide();
        }

        public void Play()
        {
            Show();
            Prepare();

            _sequence.Kill();
            _sequence = DOTween.Sequence();

            _sequence.Append(_rectTransform.DOScale(1f, 0.2f).SetEase(moveCurve));
            _sequence.Join(_rectTransform.DOAnchorPosY(_rectTransform.anchoredPosition.y + _moveUpDistance, _duration).SetEase(moveCurve));
            _sequence.Join(_canvasGroup.DOFade(0f, _duration).SetEase(moveCurve));

            _sequence.OnComplete(Hide);
        }

        private void Prepare()
        {
            _rectTransform.anchoredPosition = _startPos;
            _rectTransform.localScale = Vector3.zero;
        }

        private void Hide()
        {
            _canvasGroup.alpha = 0f;
        }

        private void Show()
        {
            _canvasGroup.alpha = 1f;
        }
    }
}