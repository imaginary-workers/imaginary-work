using Game.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] MoveComponent _moveComponent;
        [SerializeField] JumpComponent _jumpComponent;
        [SerializeField, Range(0, 10)] float _normalSpeed = 8f;
        [SerializeField, Range(0, 20)] float _sprintSpeed = 12f;
        [SerializeField] PlayerInput _playerInput;
        [SerializeField] PlayerAnimationManager _animator;
        Vector2 _moveVelocityInput;
        float _currentTime = 1;
        float _time;
        float _currentSpeed;
        
        public bool IsCurrentDeviceMouse
            => _playerInput.currentControlScheme == "KeyboardMouse";

        public Vector2 LookInputDirection { get; private set; }

        public float Speed
        {
            get => _currentSpeed;
            set => _currentSpeed = value;
        }

        #region unitymethods

        void Awake()
        {
            _time = _currentTime;
            _currentSpeed = _normalSpeed;
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
                _moveComponent.Velocity = (_moveVelocityInput.x * transform.right + transform.forward * _moveVelocityInput.y).normalized * _currentSpeed;
            /*if ((_currentTime < 0 || _jumpComponent.IsOnTheFloor))
            {
            }

            if (!_jumpComponent.IsOnTheFloor)
            {
                _currentTime -= Time.deltaTime;
            }
            else
            {
                _currentTime = _time;
            }*/
        }

        #endregion

        #region inputmethods

        public void MoveInput(InputAction.CallbackContext context)
        {
            _moveVelocityInput = context.ReadValue<Vector2>();
            if (context.canceled)
            {
                _moveVelocityInput = Vector2.zero;
            }
        }

        public void JumpInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _jumpComponent.JumpAction();
            }
        }

        public void LookInput(InputAction.CallbackContext context)
        {
            LookInputDirection = context.ReadValue<Vector2>();
        }

        public void SprintInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _currentSpeed = _sprintSpeed;
                _animator.StartSprint();
            }
            else if (context.canceled)
            {
                _currentSpeed = _normalSpeed;
                _animator.StopSprint();
            }
        }

        #endregion
    }
}