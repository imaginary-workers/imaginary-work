using System.Collections;
using Game.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class FollowMeleeStateController : EnemyStateController
    {
        [SerializeField] protected PatrolBehaviour _patrolBehaviour;
        [SerializeField] [Range(0, 15)] protected int _rangeFollow = 15;
        [SerializeField] [Range(.1f, 3f)] protected float _rangeOfVisionY = 1;
        [SerializeField] protected FollowPlayer _followPlayer;
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected LookAtTarget _lookAtTarget;
        [SerializeField] [Range(0f, 5f)] protected float _moveSpeed = 5f;
        [SerializeField] protected AnimationEvent _animationEvent;
        [SerializeField] protected float _secondToDestroy = 4;
        [SerializeField] protected SpawnDrops _spawn;
        [SerializeField] protected AnimatorController _animatorController;
        [SerializeField] Ragdoll _ragdoll;
        [SerializeField] protected Collider _collider;
        [SerializeField] Damaging _damaging;
        bool _canDoStrongDamageFeedback = true;
        bool _isAttacking;
        GameObject _player;
        float _rangeMelee = 0.5f;
        readonly float _takeStrongDamageRecoverTime = 3f;

        public RandomPatrolState RandomPatrolState { get; private set; }

        public FollowState FollowState { get; private set; }

        public MeleeAttackState MeleeState { get; private set; }

        public TakeStrongDamageState TakeStrongDamageState { get; private set; }

        public int RangeFollow => _rangeFollow;

        void OnEnable()
        {
            Damageable.OnTakeStrongDamage += OnTakeStrongDamageHandler;
            Damageable.OnTakeStrongDamage += OnTakeDamageHandler;
        }

        void OnDisable()
        {
            Damageable.OnTakeStrongDamage -= OnTakeDamageHandler;
            Damageable.OnTakeStrongDamage -= OnTakeStrongDamageHandler;
        }

        protected override void OnAwakeEnemy()
        {
            _damaging.EnemySource = gameObject;
            _player = GameManager.Player;
            _lookAtTarget.Target = _player.gameObject;
            _rangeMelee = _followPlayer.CloseRange;
            _patrolBehaviour.Speed = _followPlayer.Speed = _moveSpeed;
            _followPlayer.RangeOfVisionY = _rangeOfVisionY;
            RandomPatrolState = new RandomPatrolState(this, _patrolBehaviour, _player, _rangeOfVisionY);
            FollowState = new FollowState(this, _followPlayer, _lookAtTarget, _player, _rangeMelee, _rangeOfVisionY);
            MeleeState = new MeleeAttackState(this, _agent, _lookAtTarget, _animationEvent, _player, _rangeMelee,
                _rangeOfVisionY, _animatorController);
            _patrolBehaviour.enabled = false;
            _followPlayer.enabled = false;
            _lookAtTarget.enabled = false;
            TakeStrongDamageState = new TakeStrongDamageState(this, _agent, _animationEvent, _animatorController);
            ChangeState(RandomPatrolState);
        }

        void ChangeToTakeStrongDamageState()
        {
            if (TakeStrongDamageState == null) return;

            ChangeState(TakeStrongDamageState);
        }

        void OnTakeStrongDamageHandler(int damage, GameObject damaging)
        {
            if (!_canDoStrongDamageFeedback) return;
            StartCoroutine(CO_TakeStrongDamageRecoverTime());
            ChangeToTakeStrongDamageState();
        }

        void OnTakeDamageHandler(int obj, GameObject damaging)
        {
        }

        IEnumerator CO_TakeStrongDamageRecoverTime()
        {
            _canDoStrongDamageFeedback = false;
            yield return new WaitForSeconds(_takeStrongDamageRecoverTime);
            _canDoStrongDamageFeedback = true;
        }

        protected override void SetDeadState()
        {
            deadState = new DeadState(_agent, _ragdoll, _spawn, this, _secondToDestroy, _collider);
        }
    }
}