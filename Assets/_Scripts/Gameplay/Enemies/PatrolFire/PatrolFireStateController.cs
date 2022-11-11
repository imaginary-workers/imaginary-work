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
        [SerializeField] NavMeshAgent _agent;
        [SerializeField] LookAtTarget _lookAtTarget;
        [SerializeField] EnemyBurstShooter _enemyShooter;
        [SerializeField] AnimatorController _animatorController;
        [SerializeField] SpawnDrops _spawner;
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
            _enemyShooter.Target = _lookAtTarget.Target = _visualField.Target = _player;
            _visualField.enabled = true;
            _normalBehaviour.enabled = false;
            _lookAtTarget.enabled = false;
            _enemyShooter.enabled = false;
            _normalState = new NormalState(this, _normalBehaviour, _visualField);
            _attackState = new AttackState(this, _visualField, _agent, _lookAtTarget, _animatorController, _enemyShooter);
            deadState = new DeadState(this, _animatorController, 5, _agent, _spawner, HitStopEffect);
            _takeStrongDamageState = new TakeStrongDamageState(this, _agent, _animatorController, _visualField);
            ChangeState(_normalState);
            Damageable.OnTakeStrongDamage += OnTakeStrongDamageHandler;
        }

        public override void DestroyGameObject()
        {
            Damageable.OnTakeStrongDamage -= OnTakeStrongDamageHandler;
            base.DestroyGameObject();
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