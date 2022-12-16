using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] string _attackRight;
        [SerializeField] string _attackLeft;
        [SerializeField] string _idle;
        [SerializeField] string _weak;

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
            Debug.Log("Idle");
            _animator.SetTrigger(_idle);
        }

        public void Weak()
        {
            _animator.SetTrigger(_weak);
        }

        public void ResetAllTriggers()
        {
            _animator.ResetTrigger(_idle);
        }
    }
}
