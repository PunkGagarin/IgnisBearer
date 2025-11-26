using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Infrastructure.SceneManagement
{
    public class LoadingCurtain : ContentUi
    {
        [field: SerializeField]
        public float HideDuration { get; private set; } = 1.5f;

        [field: SerializeField]
        public Image image;

        public override void Show()
        {
            var color = image.color;
            color.a = 1;
            image.color = color;
            content.SetActive(true);
        }

        public async UniTask HideAsync()
        {
            var tcs = new UniTaskCompletionSource();

            image.DOFade(0, HideDuration)
                .OnComplete(() =>
                {
                    content.SetActive(false);
                    tcs.TrySetResult();
                });

            await tcs.Task;
        }

        public override void Hide()
        {
            image.DOFade(0, HideDuration)
                .OnComplete(() => { content.SetActive(false); });
        }
    }
}