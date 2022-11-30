using System;
using Game.Gameplay.SO;
using Game.SO;
using Game.UI;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] int _life = 10;
        [SerializeField] ElementSO _weakness;
        public event Action<int, GameObject> OnTakeDamage, OnTakeStrongDamage;
        public event Action<GameObject> OnDeath;
        public int Life => _life;

        public void TakeTamage(int damage, ElementSO element, GameObject damaging)
        {                      
            if (Life <= 0) return;

            if (_weakness == element)
            {
                _life -= (damage * 2);
                if (Life > 0)
                {
                    OnTakeStrongDamage?.Invoke(damage, damaging);
                    HitMarkerController.Instance.DisplayHitMarkStrong();
                }
            }
            else
            {
                _life -= damage;
                OnTakeDamage?.Invoke(damage, damaging);
                HitMarkerController.Instance.DisplayHitMarkWeak();
            }
            if (Life <= 0)
            {
                _life = 0;
                OnDeath?.Invoke(damaging);
                HitMarkerController.Instance.DisplayHitMarkDeath();
            }
        }
    }
}
