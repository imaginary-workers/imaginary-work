using Game._Scripts.Gameplay.Enemies.FlyerPatrol;
using UnityEngine;

namespace Game.Gameplay.Enemies.FlyerPatrol
{
    public class FlyerPatrolStateController : Enemy
    {
        State _currentState;
        NormalState _normalState;
        AttackState _attackState;
        DeadState _deadState;
        GameObject _target;
        [SerializeField] RaycastAttack _attack;
        [SerializeField] PatrolBehaviour _patrolBehaviour;
        [SerializeField] LookAtTarget _lookAtTarget;
        [SerializeField] MoveComponent _moveComponent;
        [SerializeField] VisualField _visualField;
        [Tooltip("Max distance from player to change between States")]
        [SerializeField] float _maxDistance;
        [SerializeField] GameObject _particle;
        [SerializeField] EnemyDamageable _damageable;
        [SerializeField] Light _light;
        [SerializeField] Light _lightFocus;
        [SerializeField] Color _attackColor = Color.red;
        [SerializeField] Color _normalColor = new Color(1, 186, 255);

        protected override void OnAwakeEnemy()
        {
            _target = FindPlayer();
            _lookAtTarget.Target = _target;
            _visualField.Target = _target;
            _normalState = new NormalState(this, _patrolBehaviour, _target, _moveComponent, _maxDistance, _lookAtTarget, _visualField, _light, _lightFocus, _normalColor);
            _attackState = new AttackState(this, _lookAtTarget, _attack,_moveComponent, _target, _visualField, _maxDistance, _light, _lightFocus, _attackColor);
            _deadState = new DeadState(_moveComponent, this);
            _attack.enabled = _lookAtTarget.enabled = false;
            _damageable.OnDeath += () => ChangeState(DeadState);
            FirstState();
        }
        
        void Update()
        {
            UpdateState();
        }

        public NormalState NormalState
            => _normalState;

        public AttackState AttackState
            => _attackState;

        public DeadState DeadState
            => _deadState;
        
        public void DestroyGameObject()
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        public void ChangeState(State state)
        {
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
        void FirstState()
        {
            _currentState = _normalState;
            _currentState.Enter();
        }
        void UpdateState()
        {
            _currentState.Update();
        }
        GameObject FindPlayer()
        {
            return GameObject.FindGameObjectWithTag("Player");
        }
    }
}
