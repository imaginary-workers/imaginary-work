﻿using UnityEditor;
using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class MetalEnemyAnimatorController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] EnemyDamageable _enemyDamagable;
        [SerializeField] MoveComponent moveComponent;

        void Awake()
        {
            _enemyDamagable.OnDeath += Death;
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
    }
}
