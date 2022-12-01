using Game.Gameplay.Player;
using Game.Config.SO;
using System;
using UnityEngine;

namespace Game.Gameplay.PowerUps
{
    public class PlayerHealerPowerUp : MonoBehaviour
    {
        [SerializeField] IntSO _playerHealth;
        [SerializeField] IntSO _playerMaxHealth;

        int _healingPower = 15;

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (_playerHealth.value >= _playerMaxHealth.value) return;
            else
            {
                other.GetComponent<PlayerSoundController>()?.Heal();
                _playerHealth.value += _healingPower;
            }
            if (_playerHealth.value > _playerMaxHealth.value)
            {
                _playerHealth = _playerMaxHealth;
            }
            Destroy(gameObject);
        }
    }
}
