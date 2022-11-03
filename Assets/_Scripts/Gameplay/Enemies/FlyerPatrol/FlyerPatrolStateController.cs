using Game._Scripts.Gameplay.Enemies.FlyerPatrol;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Enemies.FlyerPatrol
{
    public class FlyerPatrolStateController : EnemyStateController
    {
        [SerializeField] RaycastAttack _attack;
        [SerializeField] PatrolBehaviour _patrolBehaviour;
        [SerializeField] LookAtTarget _lookAtTarget;
        [SerializeField] MoveComponent _moveComponent;
        [SerializeField] VisualField _visualField;
        [Tooltip("Max distance from player to change between States")]
        [SerializeField] float _maxDistance;
        [SerializeField] GameObject _particle;
        [SerializeField] Light _light;
        [SerializeField] Light _lightFocus;
        [SerializeField] Color _attackColor = Color.red;
        [SerializeField] Color _normalColor = new Color(1, 186, 255);
        NormalState _normalState;
        AttackState _attackState;
        GameObject _target;

        protected override void OnAwakeEnemy()
        {
            _target = GameManager.Player;
            _lookAtTarget.Target = _target;
            _visualField.Target = _target;
            _normalState = new NormalState(this, _patrolBehaviour, _target, _moveComponent, _maxDistance, _lookAtTarget, _visualField, _light, _lightFocus, _normalColor);
            _attackState = new AttackState(this, _lookAtTarget, _attack,_moveComponent, _target, _visualField, _maxDistance, _light, _lightFocus, _attackColor);
            deadState = new DeadState(_moveComponent, this);
            _attack.enabled = _lookAtTarget.enabled = false;
            ChangeState(_normalState);
        }

        public NormalState NormalState
            => _normalState;

        public AttackState AttackState
            => _attackState;

        public override void DestroyGameObject()
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
            base.DestroyGameObject();
        }
    }
}
