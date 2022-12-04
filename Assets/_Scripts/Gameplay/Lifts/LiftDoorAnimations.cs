using System;
using UnityEngine;

namespace Game.Gameplay.Lifts
{
    public class LiftDoorAnimations : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        public Action OnOpened, OnClosed;
        public event Action OpenDoor;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _door;

        public void CloseDoors()
        {
            _animator.SetTrigger("CloseDoors");
        }
        public void OpenDoors()
        {
            _animator.SetTrigger("OpenDoors");
            _audioSource.PlayOneShot(_door);
        }

        public void ON_OPENED_EVENT()
        {
            OnOpened?.Invoke();
        }
        
        public void ON_CLOSED_EVENT()
        {
            OnClosed?.Invoke();
        }
    }
}
