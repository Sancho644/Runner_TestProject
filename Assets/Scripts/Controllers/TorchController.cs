using System;
using UnityEngine;

namespace Controllers
{
    public class TorchController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Light _light;

        private ParticleSystem.MainModule _particleSystemMain;

        private float _lightIntensityMax;
        private int _maxParticles;

        public Action OnLightOff;

        private void Start()
        {
            _particleSystemMain = _particleSystem.main;
            _lightIntensityMax = _light.intensity;
            _maxParticles = _particleSystemMain.maxParticles;
        }

        public void SetTorchValues(int torchBurningValue, float torchLightValue)
        {
            _particleSystemMain.maxParticles += torchBurningValue;
            _particleSystemMain.maxParticles = Mathf.Clamp(_particleSystemMain.maxParticles, 0, _maxParticles);
            _light.intensity += torchLightValue;
            _light.intensity = Mathf.Clamp(_light.intensity, 0, _lightIntensityMax);

            if (_light.intensity == 0 || _particleSystemMain.maxParticles == 0)
            {
                OnLightOff?.Invoke();
            }
        }
    }
}