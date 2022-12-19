using Game.Dialogs.SO;
using Game.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Dialogs
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] DialogEventSO _dialogEvent;
        [SerializeField] GameObject _canvas;
        [SerializeField] DialogUI _dialogUI;
        bool _onDialog = false;

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
            _onDialog = true;
            GameplayUIManager.Instance.CanPause = false;
            PlayManager.Instance.CanvasController(true, false);
            _canvas.SetActive(true);
            _dialogUI.StartDialog(dialog);
        }

        public void DialogFinished()
        {
            _onDialog = false;
            GameplayUIManager.Instance.CanPause = true;
            PlayManager.Instance.CanvasController(false, false);
            _canvas.SetActive(false);
        }
        public void NextSentenceDialog(InputAction.CallbackContext context)
        {
            if (context.performed &&_onDialog)
            {
                _dialogUI.SkipToNext();
            }
        }
    }
}