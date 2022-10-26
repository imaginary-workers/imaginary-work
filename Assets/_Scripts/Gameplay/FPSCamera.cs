using System;
using Game.Config;
using Game.Player;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public class FPSCamera : MonoBehaviour
    {
        const float _threshold = 0.01f;

        [SerializeField] Camera _camera;
        [SerializeField] PlayerController _playerController;
        [SerializeField] GameplaySettingsSO _gameplaySettingsSo;
        PlayerConfig _config;
        float _targetPitch;
        float _rotationVelocity;

        private void Awake()
        {
            UpdateConfig();
            _gameplaySettingsSo.OnChange += UpdateConfig;
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            UpdateCameraLook();
        }

        private void OnDestroy()
        {
            _gameplaySettingsSo.OnChange -= UpdateConfig;
        }

        void UpdateCameraLook()
        {
            var lookInput = _playerController.LookInputDirection;
            if (lookInput.sqrMagnitude < _threshold) return;

            var isCurrentDeviceMouse = _playerController.IsCurrentDeviceMouse;
            float deltaTimeMultiplier = isCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            _targetPitch += lookInput.y * _config.rotationSpeed * deltaTimeMultiplier * (_config.invertedYAxis ? 1 : -1);
            _rotationVelocity = lookInput.x * _config.rotationSpeed * deltaTimeMultiplier * (_config.invertedXAxis ? -1 : 1);
            _targetPitch = Utils.ClampAngle(_targetPitch, _config.bottomClamp, _config.topClamp);
            _camera.transform.localRotation = Quaternion.Euler(_targetPitch, 0.0f, 0.0f);
            _playerController.transform.Rotate(Vector3.up * _rotationVelocity);
        }

        void UpdateConfig()
        {
            _config = _gameplaySettingsSo.PlayerConfig;
        }
        
    }
}