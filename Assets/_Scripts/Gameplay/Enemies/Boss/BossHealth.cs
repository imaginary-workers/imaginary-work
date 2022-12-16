using System;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossHealth : MonoBehaviour
    {
        [SerializeField] IntSO _bossHealth;
        [SerializeField, Range(0, 1000)] int _maxHealth;
        [SerializeField, Range(0, 1000)] int _endPhase1;
        [SerializeField, Range(0, 1000)] int _endPhase2;
        [SerializeField, Range(0, 1000)] int amountToWeakBeforeEndPhase;
        EnemyDamageable[] _damageablesEnemyParts;
        int _currentPhase;
        public event Action OnTakeAnyDamage;
        public event Action OnEnterWeak;
        public bool IsImmune { get; set; } = false;
        public bool IsWeak { get; set; } = false;

        void Awake()
        {
            _currentPhase = 1;
            _bossHealth.value = _maxHealth;
            _damageablesEnemyParts = GetComponentsInChildren<EnemyDamageable>();
            foreach (var enemyPart in _damageablesEnemyParts)
            {
                enemyPart.Mortal = false;
                enemyPart.OnTakeDamage += OnTakeDamageHandler;
                enemyPart.OnTakeStrongDamage += OnTakeStrongDamageHandler;
            }
        }

        void OnDestroy()
        {
            foreach (var enemyPart in _damageablesEnemyParts)
            {
                enemyPart.OnTakeDamage -= OnTakeDamageHandler;
                enemyPart.OnTakeStrongDamage -= OnTakeStrongDamageHandler;
            }
        }

        void OnTakeStrongDamageHandler(int damage, GameObject arg2)
        {
            if (IsImmune) return;
            TakeAnyDamageHandler(damage);
        }

        void OnTakeDamageHandler(int damage, GameObject arg2)
        {
            if (IsImmune) return;
            TakeAnyDamageHandler(damage);
        }

        void TakeAnyDamageHandler(int damage)
        {
            OnTakeAnyDamage?.Invoke();
            if (IsWeak)
            {
                if (_currentPhase == 1)
                {
                    _bossHealth.value = _endPhase1;
                }
                else if (_currentPhase == 2)
                {
                    _bossHealth.value = _endPhase2;
                }
                else
                {
                    _bossHealth.value = 0;
                }
                IsWeak = false;
            }
            else
            {
                _bossHealth.value -= damage;
                if (
                    _currentPhase == 1 && _bossHealth.value > _endPhase1 &&
                    _bossHealth.value <= _endPhase1 + amountToWeakBeforeEndPhase
                    )
                {
                    OnEnterWeak?.Invoke();
                }
                else if (
                    _currentPhase == 2 && _bossHealth.value > _endPhase2 &&
                    _bossHealth.value <= _endPhase2 + amountToWeakBeforeEndPhase
                    )
                {
                    OnEnterWeak?.Invoke();
                }
                else if (
                    _currentPhase == 3 && _bossHealth.value > 0 &&
                    _bossHealth.value <= amountToWeakBeforeEndPhase
                )
                {
                    OnEnterWeak?.Invoke();
                }
            }
        }
    }
}
