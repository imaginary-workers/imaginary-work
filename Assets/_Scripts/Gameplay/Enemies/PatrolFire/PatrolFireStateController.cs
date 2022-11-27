using System.Collections;
using Game.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class PatrolFireStateController : EnemyStateController
    {
        [SerializeField] PatrolBehaviour _normalBehaviour;
        [SerializeField] VisualField _visualField;  
        [SerializeField] VisualField _visualFieldSound;  
        [SerializeField] NavMeshAgent _agent;
        [SerializeField] LookAtTarget _lookAtTarget;
        [SerializeField] EnemyBurstShooter _enemyShooter;
        [SerializeField] AnimatorController _animatorController;
        [SerializeField] SpawnDrops _spawner;
        [SerializeField] Ragdoll _ragdoll;
        [SerializeField] Collider _collider;
        NormalState _normalState;
        AttackState _attackState;
        GameObject _player;
        bool _canDoStrongDamageFeedback = true;
        float _takeStrongDamageRecoverTime = 3f;
        TakeStrongDamageState _takeStrongDamageState;

        public AttackState AttackState => _attackState;
        public NormalState NormalState => _normalState;
        public TakeStrongDamageState TakeStrongDamageState => _takeStrongDamageState;

        protected override void OnAwakeEnemy()
        {
            _player = GameManager.Player;
            _enemyShooter.Target = _lookAtTarget.Target = _visualField.Target = _visualFieldSound.Target = _player;
            _visualField.enabled = true;
            _normalBehaviour.enabled = false;
            _lookAtTarget.enabled = false;
            _enemyShooter.enabled = false;
            _normalState = new NormalState(this, _normalBehaviour, _visualField, _visualFieldSound);
            _attackState = new AttackState(this, _visualField, _agent, _lookAtTarget, _animatorController, _enemyShooter, _visualFieldSound);
            _takeStrongDamageState = new TakeStrongDamageState(this, _agent, _animatorController, _visualField, _visualFieldSound);
            ChangeState(_normalState);
            Damageable.OnTakeStrongDamage += OnTakeStrongDamageHandler;
        }

        public override void DestroyGameObject(float seconds = 0)
        {
            Damageable.OnTakeStrongDamage -= OnTakeStrongDamageHandler;
            base.DestroyGameObject(seconds);
        }

        protected override void SetDeadState()
        {
            deadState = new DeadState(this, _ragdoll, 5, _agent, _spawner, HitStopEffect, _collider);
        }

        void ChangeToTakeStrongDamageState()
        {
            if (_takeStrongDamageState == null) return;
            
            ChangeState(_takeStrongDamageState);
        }

        void OnTakeStrongDamageHandler(int damage, GameObject damaging)
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