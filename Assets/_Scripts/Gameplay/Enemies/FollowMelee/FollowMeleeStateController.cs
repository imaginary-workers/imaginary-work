using System.Collections;
using Game.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class FollowMeleeStateController : EnemyStateController
    {
        [SerializeField] protected PatrolBehaviour _patrolBehaviour;
        [SerializeField, Range(0, 15)] protected int _rangeFollow = 15;
        [SerializeField, Range(.1f, 3f)] protected float _rangeOfVisionY = 1;
        [SerializeField] protected FollowPlayer _followPlayer;
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected LookAtTarget _lookAtTarget;
        [SerializeField, Range(0f, 5f)] protected float _moveSpeed = 5f;
        [SerializeField] protected AnimationEvent _animationEvent;
        [SerializeField] protected float _secondToDestroy = 4;
        [SerializeField] protected SpawnDrops _spawn;
        [SerializeField] protected AnimatorController _animatorController;
        [SerializeField] Ragdoll _ragdoll;
        [SerializeField] protected Collider _collider;
        GameObject _player;
        RandomPatrolState _randomPatrolState;
        FollowState _followState;
        MeleeAttackState _meleeState;
        TakeStrongDamageState _takeStrongDamageState;
        float _rangeMelee = 0.5f;
        bool _isAttacking;
        float _takeStrongDamageRecoverTime = 3f;
        bool _canDoStrongDamageFeedback = true;

        public RandomPatrolState RandomPatrolState => _randomPatrolState;
        public FollowState FollowState => _followState;
        public MeleeAttackState MeleeState => _meleeState;
        public TakeStrongDamageState TakeStrongDamageState => _takeStrongDamageState;
        public int RangeFollow => _rangeFollow;

        void OnEnable()
        {
            Damageable.OnTakeStrongDamage += OnTakeStrongDamageHandler;
        }

        void OnDisable()
        {
            Damageable.OnTakeStrongDamage -= OnTakeStrongDamageHandler;
        }

        protected override void OnAwakeEnemy()
        {
            _player = GameManager.Player;
            _lookAtTarget.Target = _player.gameObject;
            _rangeMelee = _followPlayer.CloseRange;
            _patrolBehaviour.Speed = _followPlayer.Speed = _moveSpeed;
            _followPlayer.RangeOfVisionY = _rangeOfVisionY;
            _randomPatrolState = new RandomPatrolState(this, _patrolBehaviour, _player, _rangeOfVisionY);
            _followState = new FollowState(this, _followPlayer, _lookAtTarget, _player, _rangeMelee, _rangeOfVisionY);
            _meleeState = new MeleeAttackState(this, _agent, _lookAtTarget, _animationEvent, _player, _rangeMelee, _rangeOfVisionY, _animatorController);
            _patrolBehaviour.enabled = false;
            _followPlayer.enabled = false;
            _lookAtTarget.enabled = false;
            _takeStrongDamageState = new TakeStrongDamageState(this, _agent, _animationEvent, _animatorController);
            ChangeState(_randomPatrolState);
        }

        void ChangeToTakeStrongDamageState()
        {
            if (_takeStrongDamageState == null) return;
            
            ChangeState(_takeStrongDamageState);
        }

        void OnTakeStrongDamageHandler(int damage)
        {
            if (!_canDoStrongDamageFeedback) return;
            StartCoroutine(CO_TakeStrongDamageRecoverTime());
            ChangeToTakeStrongDamageState();
        }

        IEnumerator CO_TakeStrongDamageRecoverTime()
        {
            _canDoStrongDamageFeedback = false;
            yield return new WaitForSeconds(_takeStrongDamageRecoverTime);
            _canDoStrongDamageFeedback = true;
        }

        protected override void SetDeadState()
        {
            deadState = new DeadState(_agent, _ragdoll, _spawn, this, _secondToDestroy, HitStopEffect, _collider);
        }
    }
}
