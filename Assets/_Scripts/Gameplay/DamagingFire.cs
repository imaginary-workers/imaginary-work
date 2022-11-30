using System;
using Game.Gameplay.SO;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public class DamagingFire : MonoBehaviour
    {
        [SerializeField] int _damage = 0;
        [SerializeField] ElementSO _element;
        GameObject _enemySource;
        public GameObject EnemySource
        {
            set => _enemySource = value;
            get => _enemySource ? _enemySource : gameObject;
        }
        public event Action OnHit, OnStrongHit;
        void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable == null) return;
            damageable.OnTakeDamage += Hit;
            damageable.OnTakeStrongDamage += StrongHit;
            damageable.TakeTamage(_damage, _element, EnemySource);
            damageable.OnTakeDamage -= Hit;
            damageable.OnTakeStrongDamage -= StrongHit;

        }

        void Hit(int damage, GameObject damaging)
        {
            OnHit?.Invoke();
        }
        void StrongHit(int damage, GameObject damaging)
        {
            OnStrongHit?.Invoke();
        }
    }
}