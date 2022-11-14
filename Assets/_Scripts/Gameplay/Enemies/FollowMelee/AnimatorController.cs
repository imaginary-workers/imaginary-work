using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] EnemyDamageable _enemyDamagable;
        [SerializeField] NavMeshAgent _agent;
        [SerializeField] EnemyDamageable _damageable;
        [SerializeField] ParticleSystem _damageParticle;

        void Awake()
        {
            _damageParticle.Stop();
            _enemyDamagable.OnDeath += Death;
        }

        void OnEnable()
        {
            _damageable.OnTakeDamage += OnTakeDamageHandler;
        }

        void Update()
        {
            _animator.SetFloat("Speed", _agent.speed);
        }
        public void Attack()
        {
            _animator.SetTrigger("Attack");
        }
        public void Death()
        {
            _animator.SetTrigger("Death");
        }      
        public void TakeDamageFeedback()
        {
            _damageParticle.Play();
        }

        public void TakeStrongDamageFeedback()
        {
            _animator.SetTrigger("Hit");
        }

        void OnTakeDamageHandler(int damage)
        {
            TakeDamageFeedback();
        }
    }
}
