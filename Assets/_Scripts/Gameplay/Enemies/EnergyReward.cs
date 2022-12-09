using Game.Gameplay.Enemies;
using Game.Gameplay.Weapons.SO;
using Game.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnergyReward : MonoBehaviour
    {
        [SerializeField] EnemyDamageable _enemyDamageable;
        [SerializeField] WeaponSO _weaponSO;

        private void OnEnable()
        {
            _enemyDamageable.OnTakeStrongDamage += AddEnergy;
        }
        private void OnDisable()
        {
            _enemyDamageable.OnTakeStrongDamage -= AddEnergy;
        }

        private void AddEnergy(int arg1, GameObject arg2)
        {
            _weaponSO.Energy++;
        }
    }
}
