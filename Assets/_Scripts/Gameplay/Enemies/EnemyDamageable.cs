using System;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] int _life = 10;
        [SerializeField] ElementSO _weakness;
        public event Action<int> OnTakeDamage, OnTakeStrongDamage;
        public event Action OnDeath;
        public int Life => _life;

        public void TakeTamage(int damage, ElementSO element)
        {                      
            if (Life <= 0) return;

            if (_weakness == element)
            {
                _life -= (damage * 2);
                if (Life > 0)
                    OnTakeStrongDamage?.Invoke(damage);
            }
            else
            {
                _life -= damage;
                OnTakeDamage?.Invoke(damage);
            }
            if (Life <= 0)
            {
                _life = 0;
                OnDeath?.Invoke();
            }
        }
    }
}
