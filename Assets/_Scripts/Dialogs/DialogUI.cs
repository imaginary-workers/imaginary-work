using System.Collections;
using System.Collections.Generic;
using Game.Dialogs.SO;
using Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.Dialogs
{
    public class DialogUI : MonoBehaviour
    {
        Queue<DialogInfo> _sentences = new Queue<DialogInfo>();
        public Button _continueButton;
        public TextMeshProUGUI dialogueDisplay;
        public Image image;
        [SerializeField] Color _color;
        public float textSpeed;
        bool canContinue;
        int dialogueIndex;
        [SerializeField] DialogManager _dialogManager;
        DialogInfo _currentDialog;

        public void StartDialog(DialogSO dialog)
        {
            var sentences = dialog.Sentences;
            foreach (var sentence in sentences)
            {
                _sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            PlayManager.Instance.CanvasController(true, false);
            _continueButton.gameObject.SetActive(false);
            StopAllCoroutines();
            if (_sentences.Count <= 0)
            {
                _dialogManager.DialogFinished();
                return;
            }
            StartCoroutine(CO_DisplaySentence());
        }

        IEnumerator CO_DisplaySentence()
        {
            dialogueDisplay.SetText(string.Empty);
            _currentDialog = _sentences.Dequeue();
            string sentence = _currentDialog.text;
            var isSpecialCharacter = false;
            string specialCharacter = "";

            if(_currentDialog.image == null)
            {
                image.color = Color.clear;
            }
            else
            {
                image.sprite = _currentDialog.image;
                image.color = _color;
            }

            foreach (var character in sentence)
            {
                if (character == '<')
                {
                    isSpecialCharacter = true;
                }

                if (isSpecialCharacter)
                {
                    specialCharacter += character;
                }
                else
                {
                    var nextCharacter = "";
                    if (specialCharacter.Length > 0)
                    {
                        nextCharacter = specialCharacter;
                        specialCharacter = "";
                    }
                    else
                    {
                        nextCharacter += character;
                    }

                    dialogueDisplay.text += nextCharacter;
                    yield return new WaitForSeconds(1f / textSpeed);
                }

                if (character == '>')
                {
                    isSpecialCharacter = false;
                }
            }

            _currentDialog = null;
            UpdateButtonUI();
        }

        void UpdateButtonUI()
        {
            _continueButton.gameObject.SetActive(true);
            _continueButton.Select();
            if (_sentences.Count > 0)
            {
                //TODO display next button
            }
            else
            {
                //TODO display quit button
            }
        }

        public void Funcionaaaa()
        {
            if (_currentDialog == null)
            {
                DisplayNextSentence();
            }
            else
            {
                StopAllCoroutines();
                dialogueDisplay.text += _currentDialog.text;
                _currentDialog = null;
            }
        }
    }
}