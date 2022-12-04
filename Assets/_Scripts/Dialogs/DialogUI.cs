using System.Collections;
using System.Collections.Generic;
using Game.Dialogs.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogs
{
    public class DialogUI : MonoBehaviour
    {
        Queue<string> _sentences = new Queue<string>();
        public Button _continueButton;
        public TextMeshProUGUI dialogueDisplay;
        public float textSpeed;
        bool canContinue;
        int dialogueIndex;

        public void StartDialog(DialogSO dialog)
        {
            _continueButton.gameObject.SetActive(false);
            var sentences = dialog.Sentences;
            foreach (var sentence in sentences)
            {
                _sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_sentences.Count <= 0) return;
            StartCoroutine(CO_DisplaySentence());
        }

        private IEnumerator CO_DisplaySentence()
        {
            dialogueDisplay.SetText(string.Empty);
            string sentence = _sentences.Dequeue() as string;
            var isSpecialCharacter = false;
            string specialCharacter = "";
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

            UpdateButtonUI();
        }

        private void UpdateButtonUI()
        {
            _continueButton.gameObject.SetActive(true);
            if (_sentences.Count > 0)
            {
                //TODO display next button
            }
            else
            {
                //TODO display quit button
            }
        }
    }
}