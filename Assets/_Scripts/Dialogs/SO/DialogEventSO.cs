using System;
using UnityEngine;

namespace Game.Dialogs.SO
{
    [CreateAssetMenu(fileName = "DialogEvent", menuName = "Scriptable Object/dialog/event", order = 0)]
    public class DialogEventSO : ScriptableObject
    {
        private event Action<DialogSO> _dialogEvent;

        public void Raise(DialogSO dialog)
        {
            _dialogEvent?.Invoke(dialog);
        }

        public void Subscribe(Action<DialogSO> listener)
        {
            _dialogEvent += listener;
        }

        public void Unsubscribe(Action<DialogSO> listener)
        {
            _dialogEvent -= listener;
        }
    }
}