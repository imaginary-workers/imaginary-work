using Game.Dialogs.SO;
using UnityEngine;

namespace Game.Dialogs
{
    public class DialogEmitter : MonoBehaviour
    {
        [SerializeField] private DialogEventSO _dialogEvent;
        [SerializeField] private DialogSO _dialog;

        [ContextMenu("Emit Dialog")]
        public void Emit()
        {
            _dialogEvent.Raise(_dialog);
        }
    }
}