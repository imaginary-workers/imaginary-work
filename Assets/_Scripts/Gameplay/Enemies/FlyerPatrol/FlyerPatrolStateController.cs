using Game._Scripts.Gameplay.Enemies.FlyerPatrol;
using Game.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FlyerPatrol
{
    public class FlyerPatrolStateController : EnemyStateController
    {
        [SerializeField] RaycastAttack _attack;
        [SerializeField] PatrolBehaviour _patrolBehaviour;
        [SerializeField] GameObject _cameraMesh;
        [SerializeField] GameObject _cameraBaseMesh;
        [SerializeField] NavMeshAgent _agent;
        [SerializeField] VisualField _visualField;
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
            _visualField.Target = _target;
            _normalState = new NormalState(this, _agent, _patrolBehaviour, _cameraMesh, _cameraBaseMesh, _visualField, _light, _lightFocus, _normalColor);
            _attackState = new AttackState(this, _target, _cameraMesh, _cameraBaseMesh, _attack,_agent, _visualField, _light, _lightFocus, _attackColor);
            _attack.enabled = false;
            ChangeState(_normalState);
        }

        public NormalState NormalState
            => _normalState;

        public AttackState AttackState
            => _attackState;

        protected override void HitStopEffect()
        {
            StartCoroutine(Utils.CO_HitStop(0.3f, 0.001f, ActiveDestroyFeedback));
        }

        void ActiveDestroyFeedback()
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
            DestroyGameObject();
        }

        protected override void SetDeadState()
        {
            deadState = new DeadState(_agent, this, HitStopEffect);
        }
    }
}
