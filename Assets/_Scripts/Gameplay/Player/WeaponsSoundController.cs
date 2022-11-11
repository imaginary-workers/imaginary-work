using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class WeaponsSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _switch;

        public void SwitchWeapon()
        {
            _audioSource.PlayOneShot(_switch);
        }
    }
}
