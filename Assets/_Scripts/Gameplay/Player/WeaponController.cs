using Game.Gameplay.Weapons;
using Game.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Gameplay.Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] WeaponManager _manager;
        [SerializeField] PointerTarget _pointerTarget;
        [SerializeField] PlayerController _playerController;
        [SerializeField] [Range(0, 2)] float _speedWeaponHeavy = 1;
        [SerializeField] PlayerAnimationManager _animation;
        public bool active = true;

        [SerializeField] PlayerSoundController _pjSoundController;
        bool _switch;

        public bool CanAttack { get; set; } = true;

        Weapon CurrentWeapon => _manager.CurrentWeapon;

        void Start()
        {
            _animation.AddAnimationEvent("end_reload_weapon", EVENT_END_RELOAD_WEAPON);
        }

        public void AttackInput(InputAction.CallbackContext context)
        {
            if (!active) return;
            if (!CanAttack) return;
            CurrentWeapon.Target = _pointerTarget.transform;
            if (context.started)
            {
                _playerController.CanSprint = false;
                CurrentWeapon.StartAttack();
            }

            if (context.performed)
                CurrentWeapon.PerformedAttack();
            if (context.canceled)
            {
                _playerController.CanSprint = true;
                CurrentWeapon.EndAttack();
            }
        }
        
        public void AttackSpecialInput(InputAction.CallbackContext context)
        {
            if (!active) return;
            if (!CanAttack) return;
            CurrentWeapon.Target = _pointerTarget.transform;
            if (context.started)
            {
                _playerController.CanSprint = false;
                CurrentWeapon.StartSpecial();
            }

            if (context.performed)
                CurrentWeapon.PerformedSpecial();
            if (context.canceled)
            {
                _playerController.CanSprint = true;
                CurrentWeapon.EndSpecial();
            }
        }

        public void ReloadWeaponInput(InputAction.CallbackContext context)
        {
            if (!active) return;
            if (!context.performed) return;
            if (CurrentWeapon.CanReloadAmmunition())
            {
                CanAttack = false;
                _animation.StartReloading();
            }
            //TODO feedback cuando no se recarga
        }

        public bool ReloadReserveWeapons()
        {
            if (!active) return false;
            var reserve = _manager.ReloadReserveWeapons();
            if (reserve) UpdateAmmoUI();
            return reserve;
        }

        public void SwitchWeapon(int slot)
        {
            if (!active) return;
            if (_manager.CurrentSlot == slot) return;
            _animation.CancelAttact();
            CurrentWeapon.CancelAttack();
            if (!_manager.SwitchWeapon(slot)) return;

            _animation.BackToIdle();
            CanAttack = true;
            _playerController.CanSprint = true;
            UpdateSlotUI(slot);
            UpdateAmmoUI();
        }

        void UpdateSlotUI(int slot)
        {
            GameManager.Instance.SetActiveSlot(slot);
        }

        void UpdateAmmoUI()
        {
            GameManager.Instance.UpdateBulletCounter(CurrentWeapon.Ammunition);
            GameManager.Instance.UpdateReserveCounter(CurrentWeapon.ReserveAmmunition);
        }

        void EVENT_END_RELOAD_WEAPON()
        {
            CurrentWeapon.ReloadAmmunition();
            UpdateAmmoUI();
            CanAttack = true;
        }
    }
}