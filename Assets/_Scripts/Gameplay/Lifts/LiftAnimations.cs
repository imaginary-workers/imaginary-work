using System;
using UnityEngine;

namespace Game.Gameplay.Lifts
{
    public class LiftAnimations : MonoBehaviour
    {
        [SerializeField] Animator _animator;

        public Action OnUpFinished;

        public void UP_FINISHED_EVENT()
        {
            OnUpFinished.Invoke();
        }
        

        public void Depart()
        {
            _animator.SetTrigger("Depart");
        }

        public void Arrive()
        {
            _animator.SetTrigger("Arrive");
        }
    }
}