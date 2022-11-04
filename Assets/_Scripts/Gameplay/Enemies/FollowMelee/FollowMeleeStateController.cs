using System;
using System.Collections;
using Game.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class FollowMeleeStateController : EnemyStateController
    {
        [SerializeField] PatrolBehaviour _patrolBehaviour;
        [SerializeField, Range(0, 15)] int _rangeFollow = 15;
        [SerializeField, Range(.1f, 3f)] float _rangeOfVisionY = 1;
        [SerializeField] FollowPlayer _followPlayer;
        [SerializeField] MeleeAttack _meleeAttack;
        [SerializeField] NavMeshAgent _agent;
        [SerializeField] LookAtTarget _lookAtTarget;
        [SerializeField, Range(0f, 5f)] float _moveSpeed = 5f;
        [SerializeField] AnimationEvent _animationEvent;
        [SerializeField] float _secondToDestroy = 4;
        [SerializeField] SpawnDrops _spawn;
        [SerializeField] AnimatorController _aniController;
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
            _meleeState = new MeleeAttackState(this, _meleeAttack, _agent, _lookAtTarget, _animationEvent, _player, _rangeMelee, _rangeOfVisionY);
            _patrolBehaviour.enabled = false;
            _followPlayer.enabled = false;
            _lookAtTarget.enabled = false;
            _meleeAttack.enabled = false;
            deadState = new DeadState(_agent, _aniController, _spawn, this, _secondToDestroy);
            _takeStrongDamageState = new TakeStrongDamageState(this, _agent, _animationEvent, _aniController);
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
    }

}
