using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossStateController : Enemy
    {
        [SerializeField] float _speed;
        [SerializeField] float _minAttackTime;
        [SerializeField] float _maxAttackTime;
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
            IdleState = new IdleState(this, _speed, _player.transform, _minAttackTime, _maxAttackTime);
            AttackState = new AttackState(this);
            AttackComboState = new AttackComboState(this);
            AttackDistanceState = new AttackDistanceState(this);
            SpawnState = new SpawnState(this);
            WeakState = new WeakState(this);
            DeadState = new DeadState();
            ChangeState(IdleState);
        }

        void Update()
        {
            _currentState.Update();
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
