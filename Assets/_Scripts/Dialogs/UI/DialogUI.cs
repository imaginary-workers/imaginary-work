using System.Collections;
using System.Collections.Generic;
using Game.Dialogs.SO;
using Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogs.UI
{
    public class DialogUI : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] DialogManager _dialogManager;
        [SerializeField] GameObject _continueFeedback;
        [SerializeField] TextMeshProUGUI _dialogTextDisplay;
        [SerializeField] Image _image;
        [SerializeField] Color _color;
        [Header("Config")]
        [SerializeField] float _textSpeed;
        Queue<DialogInfo> _sentences = new Queue<DialogInfo>();
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
            _continueFeedback.gameObject.SetActive(false);
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
            _dialogTextDisplay.SetText(string.Empty);
            _currentDialog = _sentences.Dequeue();
            string sentence = _currentDialog.Text;
            var isSpecialCharacter = false;
            string specialCharacter = "";

            if(_currentDialog.Image == null)
            {
                _image.color = Color.clear;
            }
            else
            {
                _image.sprite = _currentDialog.Image;
                _image.color = _color;
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

                    _dialogTextDisplay.text += nextCharacter;
                    yield return new WaitForSeconds(1f / _textSpeed);
                }

                if (character == '>')
                {
                    isSpecialCharacter = false;
                }
            }

            _currentDialog = null;
            UpdateFeedbackContinue();
        }

        void UpdateFeedbackContinue()
        {
            _continueFeedback.gameObject.SetActive(true);
            if (_sentences.Count > 0)
            {
                //TODO display next button
            }
            else
            {
                //TODO display quit button
            }
        }

        public void Continue()
        {
            if (_currentDialog == null)
            {
                DisplayNextSentence();
            }
            else
            {
                StopAllCoroutines();
                _dialogTextDisplay.text = _currentDialog.Text;
                _currentDialog = null;
            }
        }
    }
}