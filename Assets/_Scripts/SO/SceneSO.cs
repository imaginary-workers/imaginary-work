using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    [CreateAssetMenu(fileName = "Some_Scene", menuName = "Scriptable Object/config/scene", order = 0)]
    public class SceneSO : ScriptableObject
    {
        [SerializeField]string _sceneName;
        [SerializeField] AudioClip _audioClip;
        [SerializeField] bool _playOnAwake=true;
    }
}
