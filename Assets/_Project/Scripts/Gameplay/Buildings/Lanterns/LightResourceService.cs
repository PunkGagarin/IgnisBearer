using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    public class LightResourceService
    {

        private readonly List<LightResource> _lights = new();

        public event Action<LightResource> OnLightCreated = delegate { };
        public event Action<LightResource> OnLightResourceClicked = delegate { };

        public void RegisterResource(LightResource lightResource)
        {
            _lights.Add(lightResource);

            lightResource.OnClicked += OnLightResourceClickedHandle;
            lightResource.OnHarvested += DestroyResource;

            OnLightCreated.Invoke(lightResource);
        }

        private void DestroyResource(LightResource lightResource)
        {
            _lights.Remove(lightResource);

            lightResource.OnClicked -= OnLightResourceClickedHandle;
            lightResource.OnHarvested -= DestroyResource;

            Object.Destroy(lightResource.gameObject);
        }

        private void OnLightResourceClickedHandle(LightResource resource)
        {
            OnLightResourceClicked.Invoke(resource);
        }

        public List<LightResource> GetUnharvestedResources()
        {
            return _lights;
        }
    }
}