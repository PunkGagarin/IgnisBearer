using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitMover : MonoBehaviour
    {
        public UniTask MoveTo(Vector3 destination)
        {
            var task = transform.DOMove(destination, 1f).SetSpeedBased().ToUniTask();
            return task;
        }
    }
}