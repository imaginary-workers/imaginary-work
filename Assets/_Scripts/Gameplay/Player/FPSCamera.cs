﻿using EZCameraShake;
using Game.Config;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class FPSCamera : MonoBehaviour
    {
        const float _threshold = 0.01f;

        [SerializeField, Range(0, 10f)] float _magnitud;
        [SerializeField, Range(0, 10f)] float _roughness;
        [SerializeField, Range(0, 10f)] float _fadeOutTime;
        [SerializeField] PlayerController _playerController;
        [SerializeField] GameplaySettingsSO _gameplaySettingsSo;
        [SerializeField] PlayerDamageable _pjDamageable;
        PlayerConfig _config;
        float _targetPitch;
        float _rotationVelocity;

        void Awake()
        {
            UpdateConfig();
            _gameplaySettingsSo.OnChange += UpdateConfig;
            _pjDamageable.OnTakeDamage += ShackeCamera;
        }

        void ShackeCamera(int obj, GameObject damaging)
        {
            CameraShaker.Instance.ShakeOnce(_magnitud, _roughness, 0, _fadeOutTime);
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            UpdateCameraLook();
        }

        void OnDestroy()
        {
            _gameplaySettingsSo.OnChange -= UpdateConfig;
        }

        void UpdateCameraLook()
        {
            var lookInput = _playerController.LookInputDirection;
            if (lookInput.sqrMagnitude < _threshold) return;

            float deltaTimeMultiplier = Time.deltaTime;
            _targetPitch += lookInput.y * _config.rotationSpeed * deltaTimeMultiplier * (_config.invertedYAxis ? 1 : -1);
            _rotationVelocity = lookInput.x * _config.rotationSpeed * deltaTimeMultiplier * (_config.invertedXAxis ? -1 : 1);
            _targetPitch = Utils.ClampAngle(_targetPitch, _config.bottomClamp, _config.topClamp);
            transform.localRotation = Quaternion.Euler(_targetPitch, 0.0f, 0.0f);
            _playerController.transform.Rotate(Vector3.up * _rotationVelocity);
        }

        void UpdateConfig()
        {
            _config = _gameplaySettingsSo.PlayerConfig;
        }
        
    }
}