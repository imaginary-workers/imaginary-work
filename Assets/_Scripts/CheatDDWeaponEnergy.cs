using Game.Gameplay.Weapons.SO;
using Game.Managers;
using UnityEngine;

namespace Game
{
    public class CheatDDWeaponEnergy: MonoBehaviour
    {
        public WeaponSO _weaponSO;
        

        public void AddFullEnergy()
        {
            Debug.Log("AddFullEnergy");
            _weaponSO.Energy = _weaponSO.MaxEnergy;
            GameplayUIManager.Instance.UpdateEnergyBar(_weaponSO.Energy, _weaponSO.MaxEnergy);
        }
    }
}