using System;
using System.Collections.Generic;
using Game.Dialogs.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogs
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] DialogEventSO _dialogEvent;
        [SerializeField] TextMesh _text;
        [SerializeField] Button _button;
        [SerializeField] Animator _dialogAnimator;
        Queue<string> sentences;

        private void OnEnable()
        {
            _dialogEvent.Subscribe(DialogHandler);
        }

        private void OnDisable()
        {
            _dialogEvent.Unsubscribe(DialogHandler);
        }

        private void DialogHandler(DialogSO dialog)
        {
            ShowDialogBoxUI();
            PrintNextSentences();
        }

        private void ShowDialogBoxUI()
        {
            
        }

        private void PrintNextSentences()
        {
            
        }
    }
}