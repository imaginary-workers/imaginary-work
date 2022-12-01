using System;
using Game.Gameplay.SO;
using Game.Config.SO;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] IntSO _playerLive;
        public event Action<int, GameObject> OnTakeDamage, OnTakeStrongDamage;
        public event Action<GameObject> OnDeath;

        public void TakeTamage(int damage, ElementSO element, GameObject damaging)
        {
            if (_playerLive.value - damage <= 0)
            {
                _playerLive.value = 0;
                OnDeath?.Invoke(damaging);
            }
            else
            {
                _playerLive.value -= damage;
                OnTakeDamage?.Invoke(damage, damaging);
            }
        }
    }
}