using System;
using Game.SO;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] IntSO _playerLive;
        public event Action<int> OnTakeDamage;

        public void TakeTamage(int damage, ElementSO element)
        {

            _playerLive.value -= damage;
            OnTakeDamage?.Invoke(damage);

            if (_playerLive.value <= 0)
                GameManager.Instance.DeathScreen();
        }
    }
}