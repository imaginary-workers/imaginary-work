using UnityEngine;

namespace Game.SO
{

    [CreateAssetMenu(fileName = "Some_Scene", menuName = "Scriptable Object/config/scene", order = 0)]
    public class SceneSO : ScriptableObject
    {
        [SerializeField]string _sceneName;
        [SerializeField] AudioClip _audioClip;
        [SerializeField] bool _playOnAwake=true;

        public string SceneName { get => _sceneName; }
        public AudioClip AudioClip { get => _audioClip; }
        public bool PlayOnAwake { get => _playOnAwake; }
    }
}
