using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class KamikazeStateController : EnemyStateController
    {
        [SerializeField] VisualField _visual;
        [SerializeField] VisualField _visualExplosion;
        [SerializeField] NavMeshAgent _navMesh;
        [SerializeField] AnimatorController _animatorController;
        [SerializeField] GameObject _explosionPrefab;
        GameObject _target;

        public DeadState Dead { get; private set; }
        public IdleState Idle { get; private set; }
        public FollowState Follow { get; private set; }
        public ExplosionState Explosion { get; private set; }
        [field: SerializeField] public float NormalSpeed { get; private set; }
        public GameObject Target { get; internal set; }

        protected override void OnAwakeEnemy()
        {
            _navMesh.speed = NormalSpeed;
            _target = GameManager.Player;
            _visual.Target = _target;
            _visualExplosion.Target = _target;
            Idle = new IdleState(this, _navMesh, _visual);
            Follow = new FollowState(this, _navMesh, _visual, _visualExplosion);
            Explosion = new ExplosionState(this, _navMesh, _animatorController);
            ChangeState(Idle);
        }

        protected override void SetDeadState()
        {
            Dead = new DeadState();
        }
        
        public void Explode()
        {
            Instantiate(_explosionPrefab,transform.position,Quaternion.identity);
        }
    }
}
