using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SceneStorage", menuName = "Scriptable Object/config/scene storage", order = 0)]
    public class SceneStorageSO : ScriptableObject
    {
        [SerializeField]List<SceneSO> _scenes;
        public SceneSO FindScene(string Scene)
        { 

        }

    }
}
