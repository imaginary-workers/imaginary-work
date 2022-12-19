using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class WeaponAlternativeSpecial : MonoBehaviour
    {
        [SerializeField] Weapon _weapon;

        void Start()
        {
            _weapon.Data.Energy = _weapon.Data.MaxEnergy - 2;
        }
    }
}
