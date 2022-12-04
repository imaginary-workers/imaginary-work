using Game.SO;
using UnityEngine;

namespace Game.Audio
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager singleton;
        AudioSource _audioSource;

        public AudioClip AudioMusic { get => _audioSource.clip; }
        void Awake()
        {
            if (singleton != null && singleton != this)
            {
                Destroy(gameObject);
            }
            else if (singleton == null)
            {
                singleton = this;
                DontDestroyOnLoad(gameObject);
                _audioSource = GetComponent<AudioSource>();
            }
        }
       
        public void Play(AudioClip music)
        {
            if (AudioMusic == music) return;
            PlayForBegenning(music);
        }
        public void PlayForBegenning(AudioClip music)
        {
            _audioSource.clip = music;
            _audioSource.Play();
        }
        public void UpdateMusic(SceneSO scene)
        {
            if (scene.PlayOnAwake)
            {
                MusicManager.singleton.PlayForBegenning(scene.AudioClip);
            }
            else
            {
                MusicManager.singleton.Play(scene.AudioClip);
            }
        }
    }
}
