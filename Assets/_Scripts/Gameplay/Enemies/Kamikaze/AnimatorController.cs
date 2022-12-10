using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] Animator _ani;
        [SerializeField] NavMeshAgent _agent;
        readonly Dictionary<string, Action> _events = new Dictionary<string, Action>();

        public void AddAnimationEvent(string eventName, Action callback)
        {
            if (_events.ContainsKey(eventName))
                _events[eventName] += callback;
            else
                _events.Add(eventName, callback);
        }

        public void RemoveAnimationEvent(string eventName)
        {
            _events.Remove(eventName);
        }
        public void KAMIKAZE_EVENT(string eventName)
        {
            if (!_events.ContainsKey(eventName)) return;
            _events[eventName]?.Invoke();
        }

        void Update()
        {
            _ani.SetFloat("Speed", _agent.speed);
        }
        public void AnticipationExplode()
        {
            _ani.SetTrigger("Attack");
        }

    }
}
