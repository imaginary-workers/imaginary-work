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
        [SerializeField] RangePhaseAttacks _rangePhaseAttacks = new RangePhaseAttacks();
        [Header("Attack"), Space]
        [SerializeField] AnimatorController _animatorController;
        [SerializeField] int _attackCounts = 3;
        [SerializeField] float _waitBetween;
        [SerializeField] float _waitToIdle;
        [SerializeField] string _slamLEvent;
        [SerializeField] string _slamREvent;
        [SerializeField] private BossMeleeAttack _rightHand;
        [SerializeField] private BossMeleeAttack _leftHand;
        [Header("Combo Attack")]
        [SerializeField] float _waitComboToIdle;
        [SerializeField] string comboevent;
        [Header("Weak")]
        [SerializeField] BossHealth _bossHealth;
        [SerializeField] float _waitToStaggerFinished;
        State _currentState;
        EnemyDamageable _damageable;
        State _lastState;
        GameObject _player;
        [Header("Spawn")]
        [SerializeField] string spawnIdleStartEvent;
        [SerializeField] GameObject _enemySpawn;
        [SerializeField] Transform _spawnTransform;
        [SerializeField] float _timeMax;
        [SerializeField] int _spawnEnemies;
        [SerializeField] int _rangeOfVisionOfKamikazes;
        [SerializeField] GameObject _fireSpawn;
        [Header("Shoot")]
        [SerializeField] Transform _firePoint;
        [SerializeField] ObjectPooler _bulletPooler;
        [SerializeField] string shootEvent;

        public IdleState IdleState { get; set; }
        public AttackState AttackState { get; set; }
        public AttackComboState AttackComboState { get; set; }
        public AttackDistanceState AttackDistanceState { get; set; }
        public SpawnState SpawnState { get; set; }
        public WeakState WeakState { get; set; }
        public DeadState DeadState { get; set; }

        protected override void OnAwakeEnemy()
        {
            _rangePhaseAttacks.RangePhaseAttackFilter();
            _player = GameManager.Player;
            IdleState = new IdleState(this, _animatorController, _speed, _player.transform, _minAttackTime, _maxAttackTime, _bossHealth, _rangePhaseAttacks);
            AttackState = new AttackState(this, _animatorController, _attackCounts, _waitBetween, _waitToIdle);
            AttackComboState = new AttackComboState(this, _animatorController, _waitComboToIdle, comboevent);
            AttackDistanceState = new AttackDistanceState(this, _animatorController, _firePoint, _bulletPooler, shootEvent);
            SpawnState = new SpawnState(this, _animatorController, _bossHealth, spawnIdleStartEvent, _enemySpawn, _spawnTransform, _timeMax, _spawnEnemies, _rangeOfVisionOfKamikazes, _fireSpawn);
            WeakState = new WeakState(this, _animatorController, _bossHealth, _waitToStaggerFinished);
            DeadState = new DeadState(_bossHealth);
            ChangeState(IdleState);
            _bossHealth.OnEnterWeak += ChangeToWeakState;
            _animatorController.AddAnimationEvent(_slamREvent, SlamREventHanler);
            _animatorController.AddAnimationEvent(_slamLEvent, SlamLEventHanler);
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
            _animatorController.RemoveAnimationEvent(_slamLEvent, SlamLEventHanler);
            _animatorController.RemoveAnimationEvent(_slamREvent, SlamREventHanler);
            _bossHealth.OnEnterWeak -= ChangeToWeakState;
            Destroy(gameObject, seconds);
        }
        void ChangeToDeathState(GameObject damaging)
        {
            if (DeadState == null) return;
            var deadState = DeadState as AbstractDeadState;
            deadState.Damaging = damaging;
            ChangeState(DeadState);
        }

        void SlamLEventHanler()
        {
            _leftHand.Attack();
        }
         
        void SlamREventHanler()
        {
            _rightHand.Attack();
        }
    }
}
