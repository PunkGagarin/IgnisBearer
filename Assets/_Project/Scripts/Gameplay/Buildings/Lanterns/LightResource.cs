using System;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buildings.Lanterns
{
    [RequireComponent(typeof(AutodestroyTimer))]
    public class LightResource : ClickableView<LightResource>
    {
        public Vector3 FinalPosition { get; private set; }
        
        public Action<LightResource> OnHarvested = delegate { };

        private AutodestroyTimer _timer;

        public override void Awake()
        {
            base.Awake();
            _timer = GetComponent<AutodestroyTimer>();
        }
        
        
        public void SetFinalPosition(Vector3 pos)
        {
            FinalPosition = pos;
        }

        public void Harvest()
        {
            OnHarvested.Invoke(this);
        }

        public void SetBusy()
        {
            DisableInteract();
            _timer.StopTimer();
        }
    }
}