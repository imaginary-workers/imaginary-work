using System.Collections.Generic;
using Game.Gameplay.Weapons;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class WeaponInventory : MonoBehaviour
    {
        [field: SerializeField]
        public List<Weapon> Weapons { get; private set; }
        void Awake()
        {
            foreach (var weapon in Weapons)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        public Weapon GetWeapon(int slot)
        {
            if (slot < 0 || slot >= Weapons.Count) return null;
            return Weapons[slot];
        }

        public void UnlockedWeapon(WeaponSO data)
        {
            var weapon = Weapons.Find(weapon => weapon.Data == data);
            if (weapon == null) return;
            weapon.UnLocked();
        }
    }
}