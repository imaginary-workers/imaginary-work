using Game.Gameplay.PowerUps;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] PlayerDamageable _playerdamageable;
        [SerializeField] PlayerController _playerController;
        [SerializeField] JumpComponent _jumpComponent;
        [SerializeField] AudioClip _Hurt;
        [SerializeField] AudioClip _Walking;
        [SerializeField] AudioClip _jump;
        [SerializeField] AudioClip _eating;
        [SerializeField] AudioClip _MaxAmmuation;


        private void Awake()
        {
            _jumpComponent.OnJumpEvent += Jump;
            _playerdamageable.OnTakeDamage += Damage;

        }

        private void OnDestroy()
        {
            _playerdamageable.OnTakeDamage -= Damage;
            _jumpComponent.OnJumpEvent -= Jump;
        }

        public void Damage(int damage)
        {
            _audioSource.PlayOneShot(_Hurt);
        }
        public void Walking()
        {
            _audioSource.PlayOneShot(_Walking);
        }
        public void Jump()
        {
                _audioSource.PlayOneShot(_jump);  
        }
        public void Heal()
        {
            _audioSource.PlayOneShot(_eating);
        }
        public void Amunnition()
        {
            _audioSource.PlayOneShot(_MaxAmmuation);
        }

    }
}
