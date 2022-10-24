using Game.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Move _move;
        [SerializeField] Jump _jump;
        [SerializeField, Range(0, 10)] float _speed = 8;
        [SerializeField, Range(0, 5)] float _currentTime = 1;
        [SerializeField] PlayerInput _playerInput;
        [SerializeField] WeaponController _weaponController;
        [SerializeField] float _topClamp = 90.0f;
        [SerializeField] float _bottomClamp = -90.0f;
        [SerializeField] Camera _camera;
        [SerializeField] float _rotationSpeed = 1.0f;
        [SerializeField] bool _invertedYAxis = false;
        [SerializeField] bool _invertedXAxis = false;
        float _targetPitch;
        float _rotationVelocity;
        float _time;
        private Vector2 _lookInput;
        private Vector2 _moveVelocityInput;
        private PlayerAnimationManager _playerAnimationManager;
        const float _threshold = 0.01f;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        bool IsCurrentDeviceMouse
            => _playerInput.currentControlScheme == "KeyboardMouse";
        private void Awake()
        {
            _time = _currentTime;
        }
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if ((_currentTime < 0 || _jump.IsOnTheFloor))
            {
                _move.Velocity = (_moveVelocityInput.x * transform.right + transform.forward * _moveVelocityInput.y).normalized * _speed;
            }

            if (!_jump.IsOnTheFloor)
            {
                _currentTime -= Time.deltaTime;
            }
            else if (_jump.IsOnTheFloor)
            {
                _currentTime = _time;
            }
        }

        void LateUpdate()
        {
            UpdateCameraLook();
        }

        public void Move(InputAction.CallbackContext context)
        {
            _moveVelocityInput = context.ReadValue<Vector2>();
            if (context.canceled)
            {
                _moveVelocityInput = Vector2.zero;
            }
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _jump.JumpAction();
            }
        }

        public void Look(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }

        public void Reload(InputAction.CallbackContext context)
        {

        }

        void UpdateCameraLook()
        {
            if (_lookInput.sqrMagnitude < _threshold) return;

            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            _targetPitch += _lookInput.y * _rotationSpeed * deltaTimeMultiplier * (_invertedYAxis ? 1 : -1);
            _rotationVelocity = _lookInput.x * _rotationSpeed * deltaTimeMultiplier * (_invertedXAxis ? -1 : 1);
            _targetPitch = Utils.ClampAngle(_targetPitch, _bottomClamp, _topClamp);
            _camera.transform.localRotation = Quaternion.Euler(_targetPitch, 0.0f, 0.0f);
            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }
}