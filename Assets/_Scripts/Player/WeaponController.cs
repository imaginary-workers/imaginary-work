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
        public bool _active = true;
        public bool CanAttack { get; set; } = true;

        Weapon CurrentWeapon
        {
            get
            {           
                return manager.CurrentWeapon;
            }
        }

        public void AttackInput(InputAction.CallbackContext context)
        {
            if (!_active) return;
            if (!CanAttack) return;
            CurrentWeapon.Target = _pointerTarget.transform.position;
            if (context.started)
            {
                if (CurrentWeapon.IsHeavy)
                {
                    PlayerHeavy();
                }

                _playerController.CanSprint = false;
                CurrentWeapon.StartAttack();
            }
            if (context.performed)
                CurrentWeapon.PerformedAttack();
            if (context.canceled)
            {
                if (CurrentWeapon.IsHeavy)
                {
                    PlayerBackToDefault();
                }
                _playerController.CanSprint = true;
                CurrentWeapon.CancelAttack();
            }
        }

        public void ReloadWeaponInput(InputAction.CallbackContext context)
        {
            if (!_active) return;
            if (!context.performed) return;

            
            CurrentWeapon.ReloadAmmunition();
            GameManager.Instance.UpdateBulletCounter(CurrentWeapon.Ammunition);
            UpdateUI();
        }

        public bool ReloadReserveWeapons()
        {
            if (!_active) return false;
            bool reserve = manager.ReloadReserveWeapons();
            if (reserve)
            {
                UpdateUI();
            }
            return reserve;

        }

        public void SwitchWeapon(int slot)
        {
            if (!_active) return;
            CurrentWeapon.CancelAttack();
            if (CurrentWeapon.IsHeavy)
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
            GameManager.Instance.UpdateReserveCounter(CurrentWeapon.ReserveAmmunition);
        }
    }
}