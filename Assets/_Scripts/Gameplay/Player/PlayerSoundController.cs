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
        [SerializeField] AudioClip _Hurt;
        [SerializeField] AudioClip _Walking;
        [SerializeField] AudioClip _jump;
        float _time;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _playerdamageable.OnTakeDamage += Damage;
        }
        private void Update()
        {
            _time += Time.deltaTime;
        }
        private void OnDestroy()
        {
            _playerdamageable.OnTakeDamage -= Damage;
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
            if (_time >= 1)
            {
                _audioSource.PlayOneShot(_jump);
                _time = 0;
            }
        }
    }
}
