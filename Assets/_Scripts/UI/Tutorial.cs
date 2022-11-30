using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{
    public class Tutorial : MonoBehaviour
    {
        public DialogueSegment[] dialogueSegment;
        public TextMeshProUGUI enterToContinue;
        public float textSpeed;
        int dialogueIndex;
        public TextMeshProUGUI dialogueDisplay;
        bool canContinue;

        private void Start()
        {
            StartCoroutine(PlayDialogue(dialogueSegment[0].Dialogue));
        }

        private void Update()
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
                StartCoroutine(PlayDialogue(dialogueSegment[dialogueIndex ].Dialogue));
            }
        }

        IEnumerator PlayDialogue(string Dialogue)
        {
            canContinue = false;
            dialogueDisplay.SetText(string.Empty);
            for (int i = 0; i < Dialogue.Length; i++)
            {
                dialogueDisplay.text += Dialogue[i];
                yield return new WaitForSeconds(1f / textSpeed);
            }
            canContinue = true;
        }
    }

    [System.Serializable]
    public class DialogueSegment
    {
        public string Dialogue;
    }
}
