using System;
using Game.Gameplay.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public class DamagingFire : MonoBehaviour
    {
        [SerializeField] int _damage;
        [SerializeField] ElementSO _element;
        GameObject _enemySource;

        public GameObject EnemySource
        {
            set => _enemySource = value;
            get => _enemySource ? _enemySource : gameObject;
        }

        void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable == null) return;
            damageable.OnTakeDamage += Hit;
            damageable.OnTakeStrongDamage += StrongHit;
            damageable.TakeDamage(_damage, _element, EnemySource);
            damageable.OnTakeDamage -= Hit;
            damageable.OnTakeStrongDamage -= StrongHit;
        }

        public event Action OnHit, OnStrongHit;

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