using Game.Gameplay.Enemies;
using Game.Gameplay.Weapons.SO;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class EnergyReward : MonoBehaviour
    {
        [SerializeField] EnemyDamageable _enemyDamageable;
        [SerializeField] protected WeaponSO _weaponSO;

        private void OnEnable()
        {
            _enemyDamageable.OnTakeStrongDamage += AddEnergy;
        }
        private void OnDisable()
        {
            _enemyDamageable.OnTakeStrongDamage -= AddEnergy;
        }

        protected virtual void AddEnergy(int arg1, GameObject arg2)
        {
            _weaponSO.Energy++;
            GameplayUIManager.Instance.UpdateEnergyBar(_weaponSO.Energy, _weaponSO.MaxEnergy);
        }
    }
}
