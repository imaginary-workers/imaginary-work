using System;
using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class AnimationEvent : MonoBehaviour
    {
        [SerializeField] GameObject _damage;

        void Awake()
        {
            _damage.SetActive(false);
        }

        public event Action OnAttackStarts, OnAttackEnds, OnTakeStrongDamageStarts, OnTakeStrongDamageEnds;
        public event Action OnAttack;

        public void Event_StartAnimation()
        {
            OnAttackStarts?.Invoke();
        }

        public void Event_EndAnimation()
        {
            OnAttackEnds?.Invoke();
        }

        public void Event_StartHitbox()
        {
            _damage.SetActive(true);
        }

        public void Event_EndHitbox()
        {
            _damage.SetActive(false);
        }

        public void Event_StartSound()
        {
            OnAttack?.Invoke();
        }

        public void Event_TakeStrongDamageStarts()
        {
            OnTakeStrongDamageStarts?.Invoke();
        }

        public void Event_TakeStrongDamageEnds()
        {
            OnTakeStrongDamageEnds?.Invoke();
        }
    }
}