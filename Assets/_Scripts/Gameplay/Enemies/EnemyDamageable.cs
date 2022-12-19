using System;
using Game.Gameplay.SO;
using Game.UI;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] int _life = 10;
        [SerializeField] ElementSO _weakness;

        [field: SerializeField]
        public bool Mortal { get; set; } = true;
        public int Life => _life;
        public event Action<int, GameObject> OnTakeDamage, OnTakeStrongDamage;
        public event Action<GameObject> OnDeath;

        public void TakeDamage(int damage, ElementSO element, GameObject damaging)
        {
            var position = damaging.transform.position;
            if (Life <= 0) return;

            if (_weakness == element)
            {
                if (Mortal)
                    _life -= damage * 3;
                OnTakeStrongDamage?.Invoke(damage * 2, damaging);
                if (Life > 0)
                {
                    HitMarkerController.Instance.DisplayHitMarkStrong();
                }
            }
            else
            {
                if (Mortal)
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