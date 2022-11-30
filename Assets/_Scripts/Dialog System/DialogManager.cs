using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private DialogEventSO _dialogEvent;
        private Queue<string> sentences;
        [SerializeField] private TextMesh _text;
        [SerializeField] private Button _button;
        [SerializeField] private Animator _dialogAnimator;

        private void OnEnable()
        {
            
        }
    }
}
