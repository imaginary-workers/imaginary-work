using System;
using Game.Gameplay.SO;
using Game.Managers;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] IntSO _bossHealth;
        [SerializeField, Range(0, 3000)] int _maxHealth;
        [SerializeField, Range(0, 3000)] int _endPhase1;
        [SerializeField, Range(0, 3000)] int _endPhase2;
        [SerializeField, Range(0, 3000)] int amountToWeakBeforeEndPhase;
        [SerializeField] EventSO _eventDead;
        EnemyDamageable[] _damageablesEnemyParts;
        int _currentPhase;
        public event Action OnTakeAnyDamage;
        public event Action OnEnterWeak;
        public bool IsImmune { get; set; } = false;
        public bool IsWeak { get; set; } = false;
        public int CurrentPhase { get => _currentPhase; private set => _currentPhase = value; }
        public EventSO EventDead { get => _eventDead; }
        public int Health { get => _bossHealth.value;  }
        public int MaxHealth { get => _maxHealth;  }

        public event Action<int, GameObject> OnTakeDamage;
        public event Action<int, GameObject> OnTakeStrongDamage;
        public void TakeDamage(int damage, ElementSO element, GameObject damaging) { }
        public event Action<GameObject> OnDeath;

        void Awake()
        {
            CurrentPhase = 1;
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
            TakeAnyDamageHandler(damage);
        }

        void OnTakeDamageHandler(int damage, GameObject arg2)
        {
            TakeAnyDamageHandler(damage);
        }

        void TakeAnyDamageHandler(int damage)
        {
            if (_bossHealth.value <= 0) return;
            if (IsImmune) return;
            if (IsWeak)
            {
                if (CurrentPhase == 1)
                {
                    _bossHealth.value = _endPhase1;
                }
                else if (CurrentPhase == 2)
                {
                    _bossHealth.value = _endPhase2;
                }
                else
                {
                    _bossHealth.value = 0;
                }
                CurrentPhase++;
                IsWeak = false;
                if (_bossHealth.value <= 0)
                {
                    OnDeath?.Invoke(GameManager.Player);
                }
                OnTakeAnyDamage?.Invoke();
            }
            else
            {
                _bossHealth.value -= damage;
                if (
                    CurrentPhase == 1 && _bossHealth.value > _endPhase1 &&
                    _bossHealth.value <= _endPhase1 + amountToWeakBeforeEndPhase
                    )
                {
                    OnEnterWeak?.Invoke();
                }
                else if (
                    CurrentPhase == 2 && _bossHealth.value > _endPhase2 &&
                    _bossHealth.value <= _endPhase2 + amountToWeakBeforeEndPhase
                    )
                {
                    OnEnterWeak?.Invoke();
                }
                else if (
                    CurrentPhase == 3 && _bossHealth.value > 0 &&
                    _bossHealth.value <= amountToWeakBeforeEndPhase
                )
                {
                    OnEnterWeak?.Invoke();
                }
            }
        }
    }
}
