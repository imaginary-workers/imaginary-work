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
        [SerializeField,Range(0,2)] float _speedWeaponHeavy = 1;
        private float _speedDefault;
        private Weapon _currentWeapon;

        private void Awake()
        {
            _currentWeapon = manager.CurrentWeapon;
            _speedDefault = _playerController.Speed;
        }

        public void AttackInput(InputAction.CallbackContext context)
        {
             _currentWeapon = manager.CurrentWeapon;
            _currentWeapon.Target = _pointerTarget.transform.position;
            if (context.started)
            {
                if (_currentWeapon.IsHeavy)
                {
                    _playerController.Speed = _speedWeaponHeavy;
                }
                _currentWeapon.StartAttack();
            }
            if (context.performed)
                _currentWeapon.PerformedAttack();
            if (context.canceled)
            {
                if (_currentWeapon.IsHeavy)
                {
                    _playerController.Speed = _speedDefault;
                }
            _currentWeapon.CancelAttack();
            }
        }

        public void ReloadWeaponInput(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            var weapon = manager.CurrentWeapon;
            weapon.ReloadAmmunition();
            GameManager.instance.UpdateBulletCounter(weapon.Ammunition);
        }

        public bool ReloadReserveWeapons()        
            => manager.ReloadReserveWeapons();       

        public void SwitchWeapon(int slot)
        {
            _currentWeapon.CancelAttack();
            _playerController.Speed = _speedDefault;
            manager.SwitchWeapon(slot);
        }
    }
}