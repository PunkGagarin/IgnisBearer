using _Project.Scripts.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Infrastructure.SceneManagement
{
    public class LoadingCurtain : ContentUi
    {
        [field: SerializeField]
        public float HideDuration { get; private set; } = 1.5f;

        [field: SerializeField]
        public Image image;

        private TweenerCore<Color, Color, ColorOptions> _tween;

        public override void Show()
        {
            _tween?.Kill();
            
            var color = image.color;
            color.a = 1;
            image.color = color;
            content.SetActive(true);
        }

        public async UniTask HideAsync()
        {
            var tcs = new UniTaskCompletionSource();

            await image.DOFade(0, HideDuration)
                .OnComplete(() =>
                {
                    content.SetActive(false);
                    tcs.TrySetResult();
                });

            await tcs.Task;
        }

        public override void Hide()
        {
            // DOTween.KillAll();
            _tween = image.DOFade(0, HideDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => { content.SetActive(false); });
        }
    }
}