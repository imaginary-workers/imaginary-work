
using Game.Gameplay.Weapons;
using Game.Managers;
using System;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] WeaponInventory _inventory;
        [SerializeField] PlayerAnimationManager _animationManager;
        public event Action WeaponChange;

        public int CurrentSlot { get; private set; } = 0;
        public Weapon CurrentWeapon { get; private set; }
        void Awake()
        {
            var weapon = _inventory.GetWeapon(CurrentSlot);
            if (weapon != null)
            {
                CurrentWeapon = weapon;
                CurrentWeapon.gameObject.SetActive(true);
                SubscribeWeaponsAnimations();
            }
        }

        void Start()
        {
            GameManager.Instance.UpdateBulletCounter(CurrentWeapon.Ammunition);
        }

        public bool SwitchWeapon(int slot)
        {
            if (slot == CurrentSlot) return false;

            var weapon = _inventory.GetWeapon(slot);
            if (weapon.IsLocked) return false;
            CurrentSlot = slot;
            if (weapon == null) return false;

            CurrentWeapon.gameObject.SetActive(false);
            CurrentWeapon = weapon;
            WeaponChange?.Invoke();
            CurrentWeapon.gameObject.SetActive(true);
            GameManager.Instance.UpdateBulletCounter(CurrentWeapon.Ammunition);
            return true;
        }

        public bool ReloadReserveWeapons()
        {
            var itReloadSomeWeapon = false;
            foreach (var weapon in _inventory.Weapons)
            {
                var itReload = weapon.ReloadReserveAmmunition();
                if (!itReloadSomeWeapon)
                    itReloadSomeWeapon = itReload;
            }

            return itReloadSomeWeapon;
        }

        void SubscribeWeaponsAnimations()
        {
            foreach (var weapon in _inventory.Weapons)
            {
                weapon.SubscribeToAnimationEvents(_animationManager);
            }
        }
    }
}