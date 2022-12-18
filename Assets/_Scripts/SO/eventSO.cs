using System;
using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "eventSO", menuName = "Scriptable Object/values/event", order = 0)]
    public class eventSO : ScriptableObject
    {
        event Action events;

        public void Raise()
        {
            Debug.Log("Raise");
            events?.Invoke();
        }
        public void RegisterEvent(Action callback)
        {
            Debug.Log("registrado");
            events += callback;

        }
        public void Unregister(Action callback)
        {
            events -= callback;
        }
    }
}
