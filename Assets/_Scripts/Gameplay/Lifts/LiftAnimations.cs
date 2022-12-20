using System;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Lifts
{
    public class LiftAnimations : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _liftSound;
        public Action OnUpFinished;

        public void UP_FINISHED_EVENT()
        {
            OnUpFinished.Invoke();
            PlayManager.Instance.SetPlayerControlActive(true);
        }


        public void Depart()
        {
            _animator.SetTrigger("Depart");
        }

        public void Arrive()
        {
            _animator.SetTrigger("Arrive");
            _audioSource.PlayOneShot(_liftSound);
        }
    }
}