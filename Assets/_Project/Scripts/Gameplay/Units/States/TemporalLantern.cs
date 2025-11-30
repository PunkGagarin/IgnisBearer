using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class TemporalLantern : MonoBehaviour
    {
        
        private bool _isFired;
        private bool _isReadyToHarvest = true;


        private SpriteRenderer _lanternSprite;
        
        private void Awake()
        {
            _lanternSprite = GetComponentInChildren<SpriteRenderer>();
        }

        public Vector3 GetPosition()
        {
            return Vector3.zero;
        }

        public bool IsReadyToHarvest()
        {
            return _isReadyToHarvest;
        }

        public bool IsFired()
        {
            return _isFired;
        }
        
        public void FireUp()
        {
            _isFired = true;
            _lanternSprite.color = Color.red;
        }
    }
}