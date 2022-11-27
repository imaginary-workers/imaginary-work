using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] NavMeshAgent _agent;
        [SerializeField] Animator _animator;
        [SerializeField] EnemyDamageable _damageable;
        [SerializeField] ParticleSystem _damageParticle;
        Dictionary<string, Action> _events = new Dictionary<string, Action>();
        private void Awake()
        {
            _damageParticle.Stop();
        }
        void OnEnable()
        {
            _damageable.OnTakeDamage += OnTakeDamageHandler;
        }

        void OnDisable()
        {
            _damageable.OnTakeDamage -= OnTakeDamageHandler;
        }

        void LateUpdate()
        {
            _animator.SetFloat("Speed", _agent.speed);
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
          _damageParticle.Play();
        }
        
        public void TakeStrongDamageFeedback()
        {
            _animator.SetTrigger("Hit");
        }

        void OnTakeDamageHandler(int damage, GameObject damaging)
        {
            TakeDamageFeedback();
        }
    }
}