using System.Collections;
using System.Collections.Generic;
using Game.Gameplay.Enemies.FollowMelee;
using Game.Managers;
using Game.Player;
using UnityEngine;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class PatrolFireStateController : EnemyStateController
    {
        [SerializeField] PatrolBehaviour _normalBehaviour;
        [SerializeField] VisualField _visualField;
        [SerializeField] MonoBehaviour _attackBehaviour;  
        [SerializeField] MoveComponent _moveComponent;
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
            DesactiveBehaviours();
            _normalState = new NormalState(this, _normalBehaviour, _visualField);
            _attackState = new AttackState(this, _visualField, _moveComponent, _lookAtTarget, _animatorController, _enemyShooter);
            deadState = new DeadState(this, _animatorController, 5, _moveComponent, _spawner);
            _takeStrongDamageState = new TakeStrongDamageState(this, _moveComponent, _animatorController);
            ChangeState(_normalState);
            Damageable.OnTakeStrongDamage += OnTakeStrongDamageHandler;
        }

        public void DesactiveBehaviours()
        {
            _visualField.enabled = true;
            _normalBehaviour.enabled = false;
            _attackBehaviour.enabled = false;
            _lookAtTarget.enabled = false;
            _enemyShooter.enabled = false;
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

        public override void DestroyGameObject()
        {
            Damageable.OnTakeStrongDamage -= OnTakeStrongDamageHandler;
            base.DestroyGameObject();
        }
    }
}