using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] MoveComponent moveComponent;
        [SerializeField] Animator _animator;
        [SerializeField] EnemyDamageable _damageable;
        Dictionary<string, Action> _events = new Dictionary<string, Action>();

        void OnEnable()
        {
            _damageable.OnTakeDamage += OnTakeDamageHandler;
        }

        void OnDisable()
        {
            _damageable.OnTakeDamage -= damage => TakeDamageFeedback();
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

        public void PLAY_EVENT(string eventName)
        {
            _events[eventName]?.Invoke();   
        }

        public void AddAnimationEvent(string eventName, Action callback)
        {
            if (_events.ContainsKey(eventName)) return;
            _events.Add(eventName, callback);
        }

        public void RemoveAnimationEvent(string eventName)
        {
            _events.Remove(eventName);
        }

        public void Death()
        {
            _animator.SetTrigger("Death");
        }

        public void TakeDamageFeedback()
        {
            _animator.SetBool("Hit", true);
        }
        
        public void TakeStrongDamageFeedback()
        {
            
        }

        void OnTakeDamageHandler(int damage)
        {

            TakeDamageFeedback();
        }
    }
}