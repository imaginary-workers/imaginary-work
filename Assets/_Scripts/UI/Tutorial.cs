using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class Tutorial : MonoBehaviour
    {
        public DialogueSegment[] dialogueSegment;
        public TextMeshProUGUI enterToContinue;
        public float textSpeed;
        public TextMeshProUGUI dialogueDisplay;
        bool canContinue;
        int dialogueIndex;

        void Start()
        {
            StartCoroutine(PlayDialogue(dialogueSegment[0].Dialogue));
        }

        void Update()
        {
            enterToContinue.enabled = canContinue;
            if (canContinue)
            {
                dialogueIndex++;
                if (dialogueIndex == dialogueSegment.Length)
                {
                    gameObject.SetActive(false);
                    return;
                }

                StartCoroutine(PlayDialogue(dialogueSegment[dialogueIndex].Dialogue));
            }
        }

        IEnumerator PlayDialogue(string Dialogue)
        {
            canContinue = false;
            dialogueDisplay.SetText(string.Empty);
            for (var i = 0; i < Dialogue.Length; i++)
            {
                dialogueDisplay.text += Dialogue[i];
                yield return new WaitForSeconds(1f / textSpeed);
            }

            canContinue = true;
        }
    }

    [Serializable]
    public class DialogueSegment
    {
        public string Dialogue;
    }
}