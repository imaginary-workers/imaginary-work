using Game.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class KamikazeStateController : EnemyStateController
    {
        [SerializeField] NavMeshAgent _navMesh;
        [SerializeField] FollowPlayer _followPlayer;
        [SerializeField] AnimatorController _animatorController;
        [SerializeField] GameObject _explosionPrefab;
        [SerializeField] int _explosionDamage;
        [SerializeField] LayerMask _playerLayer;
        [SerializeField] AudioClip _explosionClip;
        [SerializeField, Range(1, 10)] float _explosionRadius;
        [SerializeField] float _rangeFollow = 10;
        [field: SerializeField]
        public float RangeFollow
        {
            get => _rangeFollow;
            set
            {
                _rangeFollow = value;
                Idle.RangeFollow = _rangeFollow;
                Follow.RangeFollow = _rangeFollow;
            }
            
        }

        [field: SerializeField]
        public float RangeOfVisionY { get; set; } = 1;
        [field: SerializeField]
        public float RangeExplosion { get; set; }
        public State Dead { get => deadState; private set => deadState = value; }
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
            Explosion = new ExplosionState(this, _navMesh, _animatorController,_explosionRadius, _playerLayer, _explosionDamage);
            _followPlayer.enabled = false;
            ChangeState(Idle);
        }

        protected override void SetDeadState()
        {
            Dead = new DeadState(this,_explosionClip);
        }
        
        public void Explode()
        {
            Instantiate(_explosionPrefab,transform.position,Quaternion.identity);
            var colliders = Physics.OverlapSphere(transform.position, _explosionRadius, _playerLayer);
            foreach (var collider in colliders)
            {
                collider.GetComponent<IDamageable>()?.TakeDamage(_explosionDamage, null, gameObject);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _explosionRadius);
        }
#endif
    }
}
