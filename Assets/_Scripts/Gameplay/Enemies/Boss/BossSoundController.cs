using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossSoundController : MonoBehaviour
    {
        [SerializeField] AnimatorController _animatorController;
        [SerializeField] EnemyDamageable _enemyDamageableWood;
        [SerializeField] EnemyDamageable enemyDamageableHead;
        [SerializeField] BossHealth _bossHealth;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _attackSlam;
        [SerializeField] AudioClip _damageWood;
        [SerializeField] AudioClip _damageGlass;
        [SerializeField] AudioClip _spawnSound;
        [SerializeField] string _slamEvent;
        [SerializeField] string _spawnFireSound;
        private void Awake()
        {
            _animatorController.AddAnimationEvent(_slamEvent, AttackSlam);
            _animatorController.AddAnimationEvent(_spawnFireSound, SpawnSound);
            _enemyDamageableWood.OnTakeDamage += DamageWood;
            _enemyDamageableWood.OnTakeStrongDamage += DamageWood;
            enemyDamageableHead.OnTakeDamage += DamageHead;
            enemyDamageableHead.OnTakeStrongDamage += DamageHead;
        }

        private void SpawnSound()
        {
            _audioSource.PlayOneShot(_spawnSound);
        }

        private void DamageHead(int arg1, GameObject arg2)
        {
            _audioSource.PlayOneShot(_damageGlass);
        }

        public void AttackSlam()
        {
            _audioSource.PlayOneShot(_attackSlam);
        }
        private void OnDestroy()
        {
            _animatorController.RemoveAnimationEvent(_slamEvent, AttackSlam);
        }
        public void DamageWood(int arg1, GameObject arg2)
        {
            _audioSource.PlayOneShot(_damageWood);
        }
    }
}
