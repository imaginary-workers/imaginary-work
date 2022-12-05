using UnityEngine;

namespace Game.Dialogs.SO
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Scriptable Object/dialog/dialog", order = 0)]
    public class DialogSO : ScriptableObject
    {
        [SerializeField] DialogInfo[] _sentences;

        public DialogInfo[] Sentences => _sentences.Clone() as DialogInfo[];
    }
}