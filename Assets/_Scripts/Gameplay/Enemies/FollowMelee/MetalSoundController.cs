using Game.Gameplay.Enemies.FollowMelee;
using Game.Gameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class MetalSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _WeakDamage;
        [SerializeField] AudioClip _Attack;
        [SerializeField] AudioClip _takeDamageFire;
        [SerializeField] AnimationEvent _aniEvent;
        [SerializeField] EnemyDamageable _enemyDamageable;
        [SerializeField] Damaging 
        void Awake()
        {
            _aniEvent.OnAttack += Attack;
            _enemyDamageable.OnTakeDamage += WeakDamage;
        }

        void OnDestroy()
        {
            _aniEvent.OnAttack -= Attack;
            _enemyDamageable.OnTakeDamage -= WeakDamage;
        }
        public void WeakDamage(int damage)
        {
            _audioSource.PlayOneShot(_WeakDamage);
        }
        public void Attack()
        {
            _audioSource.PlayOneShot(_Attack);
        }
        public void TakeDamage()
        {
            _audioSource.PlayOneShot(_takeDamageFire);
        }
    }
}
