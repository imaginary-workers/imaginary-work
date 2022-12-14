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
        [SerializeField] PlayerAnimationManager _animation;
        public bool active = true;

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
            {
                CurrentWeapon.PerformedSpecial();
                var data = CurrentWeapon.Data;
                GameplayUIManager.Instance.UpdateEnergyBar(data.Energy, data.MaxEnergy);
            }
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
            if (CurrentWeapon.CanReloadAmmunition() && CanReload)
            {
                CanReload = false;
                CanAttack = false;
                _animation.StartReloading();
            }
            //TODO feedback cuando no se recarga
        }

        public bool CanReload { get; set; } = true;

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

            CanReload = true;
            _animation.BackToIdle();
            CanAttack = true;
            _playerController.CanSprint = true;
            UpdateSlotUI(slot);
            UpdateAmmoUI();
            var data = CurrentWeapon.Data;
            GameplayUIManager.Instance.UpdateEnergyBar(data.Energy, data.MaxEnergy);
        }

        void UpdateSlotUI(int slot)
        {
            GameplayUIManager.Instance.SetActiveSlot(slot);
        }

        void UpdateAmmoUI()
        {
            GameplayUIManager.Instance.UpdateBulletCounter(CurrentWeapon.Ammunition);
            GameplayUIManager.Instance.UpdateReserveCounter(CurrentWeapon.ReserveAmmunition);
        }

        void EVENT_END_RELOAD_WEAPON()
        {
            CurrentWeapon.ReloadAmmunition();
            UpdateAmmoUI();
            CanAttack = true;
            CanReload = true;
        }
    }
}