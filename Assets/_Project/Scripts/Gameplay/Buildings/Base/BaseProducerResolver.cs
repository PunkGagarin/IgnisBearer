using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    public abstract class BaseProducerResolver : MonoBehaviour, IProduceResolver
    {
        public abstract bool CanProduce();
    }
}