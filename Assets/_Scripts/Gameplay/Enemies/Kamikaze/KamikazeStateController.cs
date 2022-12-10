using Game.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class KamikazeStateController : EnemyStateController
    {
        [SerializeField] NavMeshAgent _navMesh;
        [SerializeField] private FollowPlayer _followPlayer;
        [SerializeField] AnimatorController _animatorController;
        [SerializeField] GameObject _explosionPrefab;

        [field: SerializeField]
        public float RangeFollow { get; set; } = 15;
        [field: SerializeField]
        public float RangeOfVisionY { get; set; } = 1;
        [field: SerializeField]
        public float RangeExplosion { get; set; }
        public DeadState Dead { get; private set; }
        public IdleState Idle { get; private set; }
        public FollowState Follow { get; private set; }
        public ExplosionState Explosion { get; private set; }
        [field: SerializeField] public float NormalSpeed { get; private set; }
        public GameObject Target { get; private set; }

        protected override void OnAwakeEnemy()
        {
            _navMesh.speed = NormalSpeed;
            Target = GameManager.Player;
            Idle = new IdleState(this, _navMesh);
            Follow = new FollowState(this, _navMesh, _followPlayer);
            Explosion = new ExplosionState(this, _navMesh, _animatorController);
            _followPlayer.enabled = false;
            ChangeState(Idle);
        }

        protected override void SetDeadState()
        {
            Dead = new DeadState(this);
        }
        
        public void Explode()
        {
            Instantiate(_explosionPrefab,transform.position,Quaternion.identity);
        }
    }
}
