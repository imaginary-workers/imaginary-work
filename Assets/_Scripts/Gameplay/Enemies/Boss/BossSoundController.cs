using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossSoundController : MonoBehaviour
    {
        [SerializeField] AnimatorController _animatorController;
        [SerializeField] EnemyDamageable _enemyDamageable;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _attackSlam;
        [SerializeField] AudioClip _damageWood;
        [SerializeField] string _slamEvent;
        private void Awake()
        {
            _animatorController.AddAnimationEvent(_slamEvent, AttackSlam);
            _enemyDamageable.OnTakeDamage += DamageWood;
            _enemyDamageable.OnTakeStrongDamage += DamageWood;
        }
        public void AttackSlam()
        {
            _audioSource.PlayOneShot(_attackSlam);
        }
        private void OnDestroy()
        {
            _animatorController.RemoveAnimationEvent(_slamEvent);
        }
        public void DamageWood(int arg1, GameObject arg2)
        {
            _audioSource.PlayOneShot(_damageWood);
        }
    }
}
