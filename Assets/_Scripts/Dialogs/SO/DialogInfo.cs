using System;
using UnityEngine;


namespace Game
{
    [Serializable]
    public class DialogInfo
    {
        [TextArea(3, 5)] public string text;
        public Sprite image;
    }
}
