using Game.Player;
using UnityEngine;

namespace Game.Gameplay
{
    public class FPSCamera : MonoBehaviour
    {
        const float _threshold = 0.01f;

        [SerializeField] Camera _camera;
        [SerializeField] PlayerController _playerController;
        [SerializeField] float _topClamp = 90.0f;
        [SerializeField] float _bottomClamp = -90.0f;
        [SerializeField] float _rotationSpeed = 20f;
        [SerializeField] bool _invertedYAxis = false;
        [SerializeField] bool _invertedXAxis = false;
        float _targetPitch;
        float _rotationVelocity;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void LateUpdate()
        {
            UpdateCameraLook();
        }

        void UpdateCameraLook()
        {
            var lookInput = _playerController.LookInputDirection;
            if (lookInput.sqrMagnitude < _threshold) return;

            var isCurrentDeviceMouse = _playerController.IsCurrentDeviceMouse;
            float deltaTimeMultiplier = isCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            _targetPitch += lookInput.y * _rotationSpeed * deltaTimeMultiplier * (_invertedYAxis ? 1 : -1);
            _rotationVelocity = lookInput.x * _rotationSpeed * deltaTimeMultiplier * (_invertedXAxis ? -1 : 1);
            _targetPitch = Utils.ClampAngle(_targetPitch, _bottomClamp, _topClamp);
            _camera.transform.localRotation = Quaternion.Euler(_targetPitch, 0.0f, 0.0f);
            _playerController.transform.Rotate(Vector3.up * _rotationVelocity);
        }
        
    }
}