using System;
using System.Collections.Generic;
using UnityEngine;
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
            lightResource.OnClicked += OnLightResourceClicked;
            OnLightCreated.Invoke(lightResource);
        }
        
        public void DestroyResource(LightResource lightResource)
        {
            _lights.Remove(lightResource);
            lightResource.OnClicked -= OnLightResourceClicked;
            Object.Destroy(lightResource.gameObject);
        }
    }
}