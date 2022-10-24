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

        public void StartAttack()
        {
            _animator.SetBool("Attack", true);
        }

        public void StopAttack()
        {
            _animator.SetBool("Attack", false);
        }

        public void Death()
        {
            _animator.SetTrigger("Death");
        }
    }
}