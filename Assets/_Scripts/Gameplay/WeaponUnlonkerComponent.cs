using Game.Dialogs;
using Game.Gameplay.Player;
using Game.Gameplay.Weapons.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public class WeaponUnlonkerComponent : MonoBehaviour
    {
        [SerializeField] WeaponSO _weaponType;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (UnlockedWeapon(other))
                {
                    Destroy(gameObject);
                }
            }
        }

        protected virtual bool UnlockedWeapon(Collider other)
        {
            var component = other.GetComponent<WeaponInventory>();
            if (component != null)
            {
                component.UnlockedWeapon(_weaponType);
                return true;
            }

            return false;
        }
    }
}