using Game.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] MoveComponent moveComponent;
        [SerializeField] JumpComponent jumpComponent;
        [SerializeField, Range(0,10)] float _speed = 8f;
        [SerializeField] PlayerInput _playerInput;
        [SerializeField] WeaponController _weaponController;
        Vector2 _moveVelocityInput;
        PlayerAnimationManager _playerAnimationManager;

        public bool IsCurrentDeviceMouse
            => _playerInput.currentControlScheme == "KeyboardMouse";

        public Vector2 LookInputDirection { get; private set; }

        #region unitymethods

        void Update()
        {
            moveComponent.Velocity = (_moveVelocityInput.x * transform.right + transform.forward * _moveVelocityInput.y).normalized * _speed;
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
                jumpComponent.JumpAction();
            }
        }

        public void LookInput(InputAction.CallbackContext context)
        {
            LookInputDirection = context.ReadValue<Vector2>();
        }

        #endregion
    }
}