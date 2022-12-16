using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] string _attackRight;
        [SerializeField] string _attackLeft;
        [SerializeField] string _idle;

        public void AttackRigth()
        {
            _animator.SetTrigger(_attackRight);
        }

        public void AttackLeft()
        {
            _animator.SetTrigger(_attackLeft);
        }

        public void Idle()
        {
            _animator.SetTrigger(_idle);
        }
    }
}
