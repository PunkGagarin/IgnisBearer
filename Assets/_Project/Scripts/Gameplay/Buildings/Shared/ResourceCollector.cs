using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings
{
    [RequireComponent(typeof(IResourceStorage))]
    [RequireComponent(typeof(ResourceProducer))]
    public class ResourceCollector : MonoBehaviour
    {

        private IResourceStorage _resourceStorage;
        private ResourceProducer _producer;

        private void Awake()
        {
            _producer = GetComponent<ResourceProducer>();
            _resourceStorage = GetComponent<IResourceStorage>();
        }

        private void Start()
        {
            _producer.OnProduced += Collect;
        }

        private void OnDestroy()
        {
            _producer.OnProduced -= Collect;
        }

        private void Collect(int amountProduced)
        {
            _resourceStorage.IncrementAmount(amountProduced);
        }

    }
}