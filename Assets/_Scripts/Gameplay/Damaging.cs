using System;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public class Damaging : MonoBehaviour
    {
        [SerializeField] int _damage = 0;
        [SerializeField] ElementSO _element;
        public event Action OnHit, OnStrongHit;
        void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable == null) return;
            damageable.OnTakeDamage += Hit;
            damageable.OnTakeStrongDamage += StrongHit;
            damageable.TakeTamage(_damage, _element);
            damageable.OnTakeDamage -= Hit;
            damageable.OnTakeStrongDamage -= StrongHit;
            DestroySelf();
        }

        void DestroySelf()
        {
            gameObject.SetActive(false);
        }

        void Hit(int damage)
        {
            OnHit?.Invoke();
        }
        void StrongHit(int damage)
        {
            OnStrongHit?.Invoke();
        }
    }
}
