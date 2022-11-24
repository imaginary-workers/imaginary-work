using Game.Gameplay.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class FlyerSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _attack;
        [SerializeField] RaycastAttack _raycastAttack;
        private void Awake()
        {
            _raycastAttack.OnAttack += Attack;
        }

        private void OnDestroy()
        {
            _raycastAttack.OnAttack -= Attack;
        }
        public void Attack()
        {
            _audioSource.PlayOneShot(_attack);
        }
    }
}
