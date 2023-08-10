using Game.Gameplay.Weapons.SO;
using Game.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.DDN
{
    public class RechargeEnergy : MonoBehaviour
    {
        [SerializeField] private WeaponSO _hammerSO;
        [SerializeField] private WeaponSO _pistoSO;
        [SerializeField] private WeaponSO _flameSO;

        public void RechargeHammer(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Recharge(_hammerSO);
        }

        private void Recharge(WeaponSO weaponSO)
        {
            weaponSO.Energy++;
            GameplayUIManager.Instance.UpdateEnergyBar(weaponSO.Energy, weaponSO.MaxEnergy);
        }

        public void RechargePistol(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Recharge(_pistoSO);
        }

        public void RechargeFlame(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Recharge(_flameSO);
        }
    }
}