using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace _Project.Scripts.Utils
{
    public static class TempDoTweenUniTaskExtensions
    {
        public static UniTask ToUniTask(this Tween tween)
        {
            var tcs = new UniTaskCompletionSource();

            tween.OnComplete(() => { tcs.TrySetResult(); });

            tween.OnKill(() =>
            {
                tcs.TrySetCanceled();
            });

            return tcs.Task;
        }
    }
}