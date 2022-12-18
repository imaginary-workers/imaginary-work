using System;
using System.Collections.Generic;
using Game.Gameplay.Weapons;
using Game.Gameplay.Weapons.SO;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class WeaponInventory : MonoBehaviour
    {
        [field: SerializeField] public List<Weapon> Weapons { get; private set; }

        void Awake()
        {
            foreach (var weapon in Weapons)
            {
                weapon.Data.Energy = 0;
                weapon.gameObject.SetActive(false);
            }
        }

        public event Action OnGrab;

        public Weapon GetWeapon(int slot)
        {
            if (slot < 0 || slot >= Weapons.Count) return null;
            return Weapons[slot];
        }

        public void UnlockedWeapon(WeaponSO data)
        {
            OnGrab?.Invoke();
            var weaponIndex = Weapons.FindIndex(weapon => weapon.Data == data);
            if (weaponIndex == -1) return;
            Weapons[weaponIndex].UnLocked();
            GameManager.Instance.UnlockedWeaponUI(weaponIndex);
        }
    }
}