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
        NormalState _normal;
        AttackState _attack;
        GameObject _player;

        protected override void OnAwakeEnemy()
        {
            _player = GameManager.Player;
            _enemyShooter.Target = _lookAtTarget.Target = _visualField.Target = _player;
            DesactiveBehaviours();
            _normal = new NormalState(this, _normalBehaviour, _visualField);
            _attack = new AttackState(this, _visualField, _moveComponent, _lookAtTarget, _animatorController, _enemyShooter);
            deadState = new DeadState(this, _animatorController, 5, _moveComponent, _spawner);
            ChangeState(_normal);
        }

        public void DesactiveBehaviours()
        {
            _visualField.enabled = true;
            _normalBehaviour.enabled = false;
            _attackBehaviour.enabled = false;
            _lookAtTarget.enabled = false;
            _enemyShooter.enabled = false;
        }

        public AttackState Attack => _attack;
        public NormalState Normal => _normal;
    }
}