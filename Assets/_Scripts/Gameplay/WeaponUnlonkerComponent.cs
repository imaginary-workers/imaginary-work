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
                var component = other.GetComponent<WeaponInventory>();
                if (component != null)
                {
                    component.UnlockedWeapon(_weaponType);
                    Destroy(gameObject);
                }
            }
        }
    }
}