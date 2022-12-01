using UnityEngine;

namespace Game.Config.SO
{
    [CreateAssetMenu(fileName = "IntSO", menuName = "Scriptable Object/values/int", order = 0)]
    public class IntSO : ScriptableObject
    {
        public int value = 0;
    }
}