using Game.Gameplay.Player;
using UnityEngine;

namespace Game.Gameplay.PowerUps
{
    public class ReserveWeaponTrigger : MonoBehaviour
    {
        void OnCollisionEnter(Collision other)
        {
            var weaponController = other.gameObject.GetComponent<WeaponController>();
            if (weaponController == null) return;
            if (!weaponController.ReloadReserveWeapons()) return;

            Destroy(gameObject);
        }
    }
}
