
using Game.Gameplay.Weapons;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] WeaponInventory _inventory;
        [SerializeField] PlayerAnimationManager _animationManager;

        public Weapon CurrentWeapon { get; private set; }
        void Awake()
        {
            var weapon = _inventory.GetWeapon(0);
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

        public void SwitchWeapon(int slot)
        {
            var weapon = _inventory.GetWeapon(slot);
            if (weapon == null) return;

            CurrentWeapon.gameObject.SetActive(false);
            CurrentWeapon = weapon;
            CurrentWeapon.gameObject.SetActive(true);
            GameManager.Instance.UpdateBulletCounter(CurrentWeapon.Ammunition);
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