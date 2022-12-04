using Game.Dialogs.SO;
using UnityEngine;

namespace Game.Dialogs
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] DialogEventSO _dialogEvent;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private DialogUI _dialogUI;

        void OnEnable()
        {
            _dialogEvent.Subscribe(DialogHandler);
        }

        void OnDisable()
        {
            _dialogEvent.Unsubscribe(DialogHandler);
        }

        void DialogHandler(DialogSO dialog)
        {
            _canvas.SetActive(true);
            _dialogUI.StartDialog(dialog);
        }
    }
}