using System;
using UnityEngine;

namespace Game.Dialogs
{
    [Serializable]
    public class DialogInfo
    {
        
        [SerializeField, TextArea(3, 5)] string _text;
        [SerializeField] Sprite _image;

        public string Text => _text;
        public Sprite Image => _image;
    }
}
