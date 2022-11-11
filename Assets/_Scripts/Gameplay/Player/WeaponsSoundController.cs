using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class WeaponsSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _switch;
        [SerializeField] AudioClip _fire;
        [SerializeField] AudioClip _noHit;
        [SerializeField] AudioClip _shootPistol;
        [SerializeField] AudioClip _recoveryPistol;

        public void SwitchWeapon()
        {
            _audioSource.PlayOneShot(_switch);
        }

        public void ShootFire()
        {
            _audioSource.PlayOneShot(_fire);
        }

        public void HitFail()
        {
            _audioSource.PlayOneShot(_noHit);
        }

        public void ShootPistol()
        {
            _audioSource.PlayOneShot(_shootPistol);
        }

        public void RecoveryPistol()
        {
            _audioSource.PlayOneShot(_recoveryPistol);
        }
    }
}
