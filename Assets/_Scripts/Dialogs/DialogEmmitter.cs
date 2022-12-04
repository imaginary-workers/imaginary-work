using Game.Dialogs.SO;
using UnityEngine;

namespace Game.Dialogs
{
    public class DialogEmmitter : MonoBehaviour
    {
        [SerializeField] private DialogEventSO _dialogEvent;
        [SerializeField] private DialogSO _dialog;

        public void Emit()
        {
            _dialogEvent.Raise(_dialog);
        }
    }
}