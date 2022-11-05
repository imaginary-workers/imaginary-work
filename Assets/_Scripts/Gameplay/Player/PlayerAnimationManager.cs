using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] Animator _myAni;
        Dictionary<string, Action> _events = new Dictionary<string, Action>();

        public void AddAnimationEvent(string eventName, Action callback)
        {
            if (_events.ContainsKey(eventName)) return;
            _events.Add(eventName, callback);
        }

        public void RemoveAnimationEvent(string eventName)
        {
            _events.Remove(eventName);
        }
        /**
         *  It get call from the animation events
         */
        public void PLAYER_EVENT(string eventName)
        {
            _events[eventName]?.Invoke();
        }
        
        public void AttackMelee()
        {
            _myAni.SetTrigger("MeleeTrigger");
        }

        public void AttackShooter()
        {
            _myAni.SetTrigger("PistolTrigger");
        }

        public void StartSprint()
        {
            _myAni.SetBool("IsSprinting", true);
        }

        public void StopSprint()
        {
            _myAni.SetBool("IsSprinting", false);
        }
    }
}
