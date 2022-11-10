using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _Hurt;
        [SerializeField] PlayerDamageable _playerdamageable;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _playerdamageable.OnTakeDamage += Damage;
        }
        private void OnDestroy()
        {
            _playerdamageable.OnTakeDamage -= Damage;
        }

        public void Damage(int damage)
        {
           
            _audioSource.PlayOneShot(_Hurt);
        }        
    }
}
