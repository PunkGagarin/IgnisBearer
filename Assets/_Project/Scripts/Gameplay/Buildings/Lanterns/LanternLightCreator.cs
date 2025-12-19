using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{

    [RequireComponent(typeof(ResourceProducer))]
    public class LanternLightCreator : MonoBehaviour
    {
        [field: SerializeField]
        private LightResource LightPrefab { get; set; }

        [field: SerializeField]
        private Transform SpawnPoint { get; set; }

        [field: SerializeField]
        public float DropSpehereRadius { get; private set; } = 3f;

        [field: SerializeField]
        public float DropAnimationTime { get; private set; } = 1f;

        [field: SerializeField]
        public float YBuffer { get; private set; } = 3f;
        
        [field: SerializeField]
        public AnimationCurve AnimaCurve { get; private set; }

        //create object pool
        private ResourceProducer _resourceProducer;

        private void Awake()
        {
            _resourceProducer = GetComponent<ResourceProducer>();
        }

        private void Start()
        {
            _resourceProducer.OnProduced += CreateLight;
        }

        private void OnDestroy()
        {
            _resourceProducer.OnProduced -= CreateLight;
        }

        private void CreateLight(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var lightResource = Instantiate(LightPrefab, SpawnPoint.position, Quaternion.identity, transform);

                Vector3 endPosition = FindRandomPositionToDropResource();
                lightResource.SetFinalPosition(endPosition);

                lightResource.transform
                    .DOMove(endPosition, DropAnimationTime)
                    .SetEase(AnimaCurve)
                    .ToUniTask(cancellationToken: destroyCancellationToken);
            }
        }

        private Vector3 FindRandomPositionToDropResource()
        {
            var spawnPointPosition = SpawnPoint.position;
            var onUnitSphere = Random.onUnitSphere * DropSpehereRadius;
            Vector3 finalPosition = spawnPointPosition + onUnitSphere - new Vector3(0, YBuffer, 0);
            finalPosition.z = 0;

            return finalPosition;
        }
    }

}