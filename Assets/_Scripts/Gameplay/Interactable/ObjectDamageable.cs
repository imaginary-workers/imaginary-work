using System;
using Game.Gameplay.SO;
using UnityEngine;

namespace Game.Gameplay.Interactable
{
    public class ObjectDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] int _life = 10;
        [SerializeField] ElementSO _weakness;
        public int Life => _life;
        public event Action<int, GameObject> OnTakeDamage, OnTakeStrongDamage;
        public event Action<GameObject> OnDeath;

        public void TakeDamage(int damage, ElementSO element, GameObject damaging)
        {
            if (Life <= 0) return;

            if (_weakness == element)
            {
                _life -= damage * 2;
                if (Life > 0)
                    OnTakeStrongDamage?.Invoke(damage, damaging);
            }
            else
            {
                _life -= damage;
                OnTakeDamage?.Invoke(damage, damaging);
            }

            if (Life <= 0)
            {
                _life = 0;
                OnDeath?.Invoke(damaging);
            }
        }
    }
}