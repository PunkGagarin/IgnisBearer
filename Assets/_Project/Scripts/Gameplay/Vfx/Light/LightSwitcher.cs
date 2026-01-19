using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Project.Scripts.Gameplay.Vfx.Light
{
    public class LightSwitcher : MonoBehaviour
    {
        [SerializeField] private bool _isEnabledByDefault = true;
        [SerializeField] private float _timeToSwitch;
        [SerializeField] private List<Light2D> _lights;

        private List<float> _startIntensity = new();
        private List<Tween> _tweens = new();

        private void Start()
        {
            _startIntensity.Clear();
            _tweens.Clear();

            for (int i = 0; i < _lights.Count; i++)
            {
                var light2D = _lights[i];

                _startIntensity.Add(light2D.intensity);
                light2D.enabled = _isEnabledByDefault;
                if (!_isEnabledByDefault)
                    light2D.intensity = 0;
                _tweens.Add(null);
            }
        }

        public void SwitchOn()
        {
            for (int i = 0; i < _lights.Count; i++)
            {
                var light2D = _lights[i];
                light2D.enabled = true;

                _tweens[i]?.Kill();

                _tweens[i] = DOTween.To(
                    () => light2D.intensity,
                    x => light2D.intensity = x,
                    _startIntensity[i],
                    _timeToSwitch
                ).SetEase(Ease.InCubic);
            }
        }

        public void SwitchOff()
        {
            for (int i = 0; i < _lights.Count; i++)
            {
                var light2D = _lights[i];

                _tweens[i]?.Kill();

                _tweens[i] = DOTween.To(
                        () => light2D.intensity,
                        x => light2D.intensity = x,
                        0,
                        _timeToSwitch
                    )
                    .SetEase(Ease.InCubic)
                    .OnComplete(() => light2D.enabled = false);
            }
        }
    }
}