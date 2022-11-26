using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SandSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audio;
        [SerializeField] AudioClip _attack;

        private void Awake()
        {

        } 
        public void Attack()
        {
            _audio.PlayOneShot(_attack);
        }
    }
}
