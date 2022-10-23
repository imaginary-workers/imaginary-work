using UnityEngine;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class PatrolFireAnimatorController : MonoBehaviour
    {
        [SerializeField] MoveComponent moveComponent;
        [SerializeField] Animator _animator;
        [SerializeField] EnemyDamageable _enemyDamagable;

        void Awake()
        {
            _enemyDamagable.OnDeath += Death;
        }

        void LateUpdate()
        {
            _animator.SetFloat("Speed", moveComponent.Velocity.magnitude);
        }

        public void ShootAnimation()
            => _animator.SetTrigger("Attack");
        public void Death()
        {
            _animator.SetTrigger("Death");
        }
    }
}