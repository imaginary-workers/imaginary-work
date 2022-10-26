using Game.Gameplay.Weapon;
using Game.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] WeaponManager manager;
        [SerializeField] PointerTarget _pointerTarget;
        [SerializeField] PlayerController _playerController;
        [SerializeField, Range(0, 2)] float _speedWeaponHeavy = 1;
        private Weapon _currentWeapon;
        public bool CanAttack { get; set; } = true;

        private void Awake()
        {
            _currentWeapon = manager.CurrentWeapon;
        }

        public void AttackInput(InputAction.CallbackContext context)
        {
            if (!CanAttack) return;
            _currentWeapon = manager.CurrentWeapon;
            _currentWeapon.Target = _pointerTarget.transform.position;
            if (context.started)
            {
                if (_currentWeapon.IsHeavy)
                {
                    PlayerHeavy();
                }

                _playerController.CanSprint = false;
                _currentWeapon.StartAttack();
            }
            if (context.performed)
                _currentWeapon.PerformedAttack();
            if (context.canceled)
            {
                if (_currentWeapon.IsHeavy)
                {
                    PlayerBackToDefault();
                }
                _playerController.CanSprint = true;
                _currentWeapon.CancelAttack();
            }
        }

        public void ReloadWeaponInput(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            var weapon = manager.CurrentWeapon;
            weapon.ReloadAmmunition();
            GameManager.instance.UpdateBulletCounter(weapon.Ammunition);
            UpdateUI();
        }

        public bool ReloadReserveWeapons()
        {
            bool reserve = manager.ReloadReserveWeapons();
            if (reserve)
            {
                UpdateUI();
            }
            return reserve;

        }

        public void SwitchWeapon(int slot)
        {
            _currentWeapon.CancelAttack();
            if (_currentWeapon.IsHeavy)
                PlayerBackToDefault();
            _playerController.CanSprint = true;
            manager.SwitchWeapon(slot);
            UpdateUI();
        }

        void PlayerBackToDefault()
        {
            _playerController.Speed = _playerController.NormalSpeed;
            _playerController.CanSprint = true;
        }

        void PlayerHeavy()
        {
            _playerController.Speed = _speedWeaponHeavy;
        }
        void UpdateUI()
        {
            var weapon = manager.CurrentWeapon;
            GameManager.instance.UpdateReserveCounter(weapon.ReserveAmmunition);
        }
    }
}