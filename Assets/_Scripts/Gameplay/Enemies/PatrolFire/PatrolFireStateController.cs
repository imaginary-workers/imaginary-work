using System.Collections.Generic;
using Game.Gameplay.Enemies.FollowMelee;
using Game.Player;
using UnityEngine;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class PatrolFireStateController : MonoBehaviour
    {
        [SerializeField] PatrolBehaviour _normalBehaviour;
        [SerializeField] VisualField _visualField;
        [SerializeField] MonoBehaviour _attackBehaviour;  
        [SerializeField] MoveComponent _moveComponent;
        [SerializeField] ActionRepeater _shooterRepeater;
        [SerializeField] LookAtTarget _lookAtTarget;
        [SerializeField] EnemyShooter enemyShooter;
        [SerializeField] EnemyDamageable _damageable;
        [SerializeField] PatrolFireAnimatorController _animatorController;

        NormalState _normal;
        AttackState _attack;
        DeadState _dead;
        State _currentState;
        State _lastState;
        GameObject _player;

        void Awake()
        {
            _player = FindObjectOfType<PlayerController>()?.gameObject;
            enemyShooter.Target = _lookAtTarget.Target = _visualField.Target = _player;
            DesactiveBehaviours();
            _normal = new NormalState(this, _normalBehaviour, _visualField);
            _attack = new AttackState(this, _visualField, _moveComponent, _lookAtTarget, _animatorController);
            _dead = new DeadState(this, _animatorController, 5);
            _currentState = _normal;
        }
        
        void Start()
        {
            _currentState.Enter();
            _damageable.OnDeath += () => ChangeState(_dead);
        }

        void Update()
        {
            if (_damageable.Life > 0)
            {
                _currentState.Update();
            }
            else
            {
                _lookAtTarget.enabled = false;
                
                _moveComponent.Velocity = Vector3.zero;
            }
        }

        public void ChangeState(State nextState)
        {
            _currentState.Exit();
            _lastState = _currentState;
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

        public void DesactiveBehaviours()
        {
            _visualField.enabled = true;
            _normalBehaviour.enabled = false;
            _attackBehaviour.enabled = false;
            _shooterRepeater.enabled = false;
            _lookAtTarget.enabled = false;
            enemyShooter.enabled = false;
        }

        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }

        public AttackState Attack => _attack;
        public NormalState Normal => _normal;
        public DeadState Dead => _dead;
    }
}