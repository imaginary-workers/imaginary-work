using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossStateController : Enemy
    {
        [Header("Idle")]
        [SerializeField] float _speed;
        [SerializeField] float _minAttackTime;
        [SerializeField] float _maxAttackTime;
        [Header("Attack")]
        [SerializeField] AnimatorController _animatorController;
        [SerializeField] int _attackCounts = 3;
        [SerializeField] float _waitBetween;
        [SerializeField] float _waitToIdle;
        [Header("Combo Attack")]
        [SerializeField] float _waitComboToIdle;
        [Header("Weak")]
        [SerializeField] BossHealth _bossHealth;
        [SerializeField] float _waitToStaggerFinished;
        State _currentState;
        EnemyDamageable _damageable;
        State _lastState;
        GameObject _player;

        public IdleState IdleState { get; set; }
        public AttackState AttackState { get; set; }
        public AttackComboState AttackComboState { get; set; }
        public AttackDistanceState AttackDistanceState { get; set; }
        public SpawnState SpawnState { get; set; }
        public WeakState WeakState { get; set; }
        public DeadState DeadState { get; set; }

        protected override void OnAwakeEnemy()
        {
            _player = GameManager.Player;
            IdleState = new IdleState(this, _animatorController, _speed, _player.transform, _minAttackTime, _maxAttackTime);
            AttackState = new AttackState(this, _animatorController, _attackCounts, _waitBetween, _waitToIdle);
            AttackComboState = new AttackComboState(this, _animatorController, _waitComboToIdle);
            AttackDistanceState = new AttackDistanceState(this);
            SpawnState = new SpawnState(this);
            WeakState = new WeakState(this, _animatorController, _bossHealth, _waitToStaggerFinished);
            DeadState = new DeadState();
            ChangeState(IdleState);
            _bossHealth.OnEnterWeak += ChangeToWeakState;
        }

        void Update()
        {
            _currentState.Update();
        }

        void ChangeToWeakState()
        {
            ChangeState(WeakState);
        }

        public void ChangeState(State nextState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
                _lastState = _currentState;
            }

            _currentState = nextState;
            _currentState.Enter();
        }

        public void ChangeBackToLastState()
        {
            if (_lastState == null) return;
            _currentState.Exit();
            _currentState = _lastState;
            _lastState = null;
            _currentState.Enter();
        }

        public virtual void DestroyGameObject(float seconds = 0)
        {
            Destroy(gameObject, seconds);
        }
    }
}
