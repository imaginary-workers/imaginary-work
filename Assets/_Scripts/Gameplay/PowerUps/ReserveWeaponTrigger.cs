using Game.Gameplay.Player;
using UnityEngine;

namespace Game.Gameplay.PowerUps
{
    public class ReserveWeaponTrigger : MonoBehaviour
    {
        void OnTriggerStay(Collider other)
        {
            var weaponController = other.gameObject.GetComponent<WeaponController>();
            if (weaponController == null) return;
            if (!weaponController.ReloadReserveWeapons()) return;
            other.GetComponent<PlayerSoundController>()?.Amunnition();

            Destroy(gameObject);
        }
    }
}