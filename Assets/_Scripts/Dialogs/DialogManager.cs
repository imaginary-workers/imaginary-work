using Game.Dialogs.SO;
using Game.Managers;
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
            PlayManager.Instance.CanvasController(true, false);
            _canvas.SetActive(true);
            _dialogUI.StartDialog(dialog);
        }

        public void DialogFinished()
        {
            PlayManager.Instance.CanvasController(false, false);
            _canvas.SetActive(false);
        }
    }
}