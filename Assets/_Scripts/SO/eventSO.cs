using System;
using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "eventSO", menuName = "Scriptable Object/values/event", order = 0)]
    public class EventSO : ScriptableObject
    {
        event Action events;

        public void Raise()
        {
            events?.Invoke();
        }
        public void RegisterEvent(Action callback)
        {
            events += callback;

        }
        public void Unregister(Action callback)
        {
            events -= callback;
        }
    }
}
