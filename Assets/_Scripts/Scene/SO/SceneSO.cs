using Game.Audio;
using UnityEngine;

namespace Game.Scene.SO
{
    [CreateAssetMenu(fileName = "Some_Scene", menuName = "Scriptable Object/config/scene", order = 0)]
    public class SceneSO : ScriptableObject, IAudioClipContainer
    {
        [SerializeField] string _sceneName;
        [SerializeField] AudioClip _audioClip;
        [SerializeField] bool _playOnAwake = true;

        public string SceneName => _sceneName;
        public AudioClip AudioClip => _audioClip;
        public bool PlayOnAwake => _playOnAwake;
    }
}