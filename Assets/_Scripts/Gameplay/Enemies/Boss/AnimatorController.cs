using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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
        [SerializeField] string _spawn;
        [SerializeField] string _shoot;
        [SerializeField] string _combo;
        Dictionary<string, Action> _animationEvents = new Dictionary<string, Action>();

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

        public void Weak()
        {
            _animator.SetTrigger(_weak);
        }

        public void ResetAllTriggers()
        {
            _animator.ResetTrigger(_idle);
            _animator.ResetTrigger(_attackLeft);
            _animator.ResetTrigger(_attackRight);
            _animator.ResetTrigger(_shoot);
            _animator.ResetTrigger(_spawn);
            _animator.ResetTrigger(_weak);
            _animator.ResetTrigger(_combo);
        }

        internal void Shoot()
        {
            _animator.SetTrigger(_shoot);
        }

        public void Spawn()
        {
            _animator.SetTrigger(_spawn);
        }
        void PLAY_EVENT(string eventname)
        {
            if (!_animationEvents.ContainsKey(eventname)) return;
            _animationEvents[eventname].Invoke();
        }
        public void AddAnimationEvent(string eventName, Action callback)
        {
            if (_animationEvents.ContainsKey(eventName)) return;
            _animationEvents.Add(eventName, callback);
        }

        public void RemoveAnimationEvent(string eventName)
        {
            _animationEvents.Remove(eventName);
        }

        internal void Combo()
        {
            _animator.SetTrigger(_combo);
        }
    }
}
