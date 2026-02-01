using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Shaders
{
    public class CrackCameraShake : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource _impulse;

        [Header("Shake")] 
        [SerializeField] private float _amplitude = 0.3f;
        [SerializeField] private float _frequency = 1f;

        public void Shake()
        {
            if (!_impulse)
                return;

            _impulse.m_DefaultVelocity = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                0f
            ).normalized * _amplitude;

            _impulse.GenerateImpulseWithForce(_frequency);
        }
    }
}