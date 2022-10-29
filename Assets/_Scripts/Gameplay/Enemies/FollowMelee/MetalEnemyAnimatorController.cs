using System;
using UnityEditor;
using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class MetalEnemyAnimatorController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] EnemyDamageable _enemyDamagable;
        [SerializeField] MoveComponent moveComponent;
        [SerializeField] EnemyDamageable _damageable;

        void Awake()
        {
            _enemyDamagable.OnDeath += Death;
        }

        void OnEnable()
        {
            _damageable.OnTakeDamage += OnTakeDamageHandler;
        }

        void Update()
        {
            _animator.SetFloat("Speed", moveComponent.Velocity.magnitude);
        }
        public void Attack()
        {
            _animator.SetTrigger("Attack");
        }
        public void Death()
        {
            _animator.SetTrigger("Death");
        }      
        void TakeDamageFeedback()
        {
            _animator.SetBool("Hit", true);           
        }

        void OnTakeDamageHandler(int damage)
        {
            TakeDamageFeedback();
        }    
    }
}
