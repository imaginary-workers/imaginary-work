using Game.Dialogs.SO;
using UnityEngine;

namespace Game.Dialogs
{
    public class DialogEmitter : MonoBehaviour
    {
        [SerializeField] DialogEventSO _dialogEvent;
        [SerializeField] DialogSO _dialog;

        [ContextMenu("Emit Dialog")]
        public void Emit()
        {
            _dialogEvent.Raise(_dialog);
        }
    }
}