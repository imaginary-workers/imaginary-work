using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] MoveComponent _moveComponent;
        [SerializeField] JumpComponent _jumpComponent;
        [SerializeField, Range(0, 10)] float _normalSpeed = 8f;
        [SerializeField, Range(0, 20)] float _sprintSpeed = 12f;
        [SerializeField] PlayerInput _playerInput;
        [SerializeField] PlayerAnimationManager _animator;
        [SerializeField] WeaponController _weaponController;
        [SerializeField] Camera _camera;
        public bool active = true;
        Vector2 _moveVelocityInput;
        float _currentTime = 1;
        float _time;
        float _currentSpeed;

        [Header("Audio")]
        [SerializeField, Range(0, 2)] float _timeWalk;
        [SerializeField] PlayerSoundController _pjSoundController;
        [SerializeField, Range(0, 2)] float _timeSprint;
        bool _isMoving = false;
        float _timer = 0;
        float _timeStep;

        public bool IsCurrentDeviceMouse
            => _playerInput.currentControlScheme == "KeyboardMouse";

        public Vector2 LookInputDirection { get; private set; }

        public float Speed
        {
            get => _currentSpeed;
            set => _currentSpeed = value;
        }

        public float NormalSpeed
        {
            get => _normalSpeed;
        }

        public float SprintSpeed
        {
            get => _sprintSpeed;
        }

        public bool CanSprint { get; set; } = true;

        #region unitymethods

        void Awake()
        {
            _time = _currentTime;
            _currentSpeed = _normalSpeed;
            _timeStep = _timeWalk;
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            CallSoundWalk();

            _timer += Time.deltaTime;
            if (!_jumpComponent.IsOnTheFloor)
            {
                SprintActive(false);
                CanSprint = false;
            }
            else
            {
                CanSprint = true;
            }
            _moveComponent.Velocity = (_moveVelocityInput.x * transform.right + transform.forward * _moveVelocityInput.y).normalized * _currentSpeed;
            /*if ((_currentTime < 0 || _jumpComponent.IsOnTheFloor))
            {
            }
            //NO BORRAR ES LA INMOBILIDAD EN EL SALTO, HAY QUE ARREGLAR UN BUG PARA HABILITARLA
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
            if (!active) return;
            _moveVelocityInput = context.ReadValue<Vector2>();

            _isMoving = true;
            if (context.canceled)
            {
                _moveVelocityInput = Vector2.zero;
                _isMoving = false;
            }
        }

        public void JumpInput(InputAction.CallbackContext context)
        {
            if (!active) return;
            if (context.started)
            {
                _jumpComponent.JumpAction();
            }

        }

        public void LookInput(InputAction.CallbackContext context)
        {
            if (!active) return;
            LookInputDirection = context.ReadValue<Vector2>();
        }

        public void SprintInput(InputAction.CallbackContext context)
        {
            if (!active) return;
            if (!CanSprint) return;
            if (context.started)
            {
                SprintActive(true);
            }
            else if (context.canceled)
            {
                SprintActive(false);
            }
        }

        #endregion

        void SprintActive(bool active)
        {
            _weaponController.CanAttack = !active;
            if (active)
            {
               // _camera.fieldOfView = 120;
                _currentSpeed = _sprintSpeed;
                _timeStep = _timeSprint;
                _animator.StartSprint();
            }
            else
            {
                //_camera.fieldOfView = 90;
                _currentSpeed = _normalSpeed;
                _timeStep = _timeWalk;
                _animator.StopSprint();
            }
        }

        void CallSoundWalk()
        {
            if (_isMoving && _timer >= _timeStep && _jumpComponent.IsOnTheFloor)
            {
                _pjSoundController.Walking();
                _timer = 0;
            }
        }
    }
}
