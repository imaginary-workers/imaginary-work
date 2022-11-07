using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SceneStorage", menuName = "Scriptable Object/config/scene storage", order = 0)]
    public class SceneStorageSO : ScriptableObject
    {
        [SerializeField]List<SceneSO> _scenes = new List<SceneSO>();
        public SceneSO FindSceneByName(string name)
        {
           return _scenes.Find((scene) => scene.SceneName == name);               
        }

    }
}
