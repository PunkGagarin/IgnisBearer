using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Buildings
{
    public class FloatingMessage : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _text;
        [Header("Animation")]
        [SerializeField] private float _moveUpDistance;
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve moveCurve;
        [Header("Random Start Offset")]
        [SerializeField] private bool _useRandomOffset = false;
        [SerializeField] private float _randomRadius = .2f;

        private Sequence _sequence;
        private Vector2 _startPos;

        private void Start()
        {
            _startPos = _rectTransform.anchoredPosition;
            Hide();
        }

        private void OnDestroy()
        {
            _sequence.Kill();
        }

        public void Play(string message)
        {
            _text.text = message;
            Play();
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
            Vector2 offset = Vector2.zero;

            if (_useRandomOffset) 
                offset = Random.insideUnitCircle * _randomRadius;

            _rectTransform.anchoredPosition = _startPos + offset;
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