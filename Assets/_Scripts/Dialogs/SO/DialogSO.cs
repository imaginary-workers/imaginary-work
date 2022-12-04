using UnityEngine;

namespace Game.Dialogs.SO
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Scriptable Object/dialog/dialog", order = 0)]
    public class DialogSO : ScriptableObject
    {
        [SerializeField, TextArea(3, 5)] string[] _sentences;

        public string[] Sentences => _sentences.Clone() as string[];
    }
}