using Game.Scene.SO;
using UnityEngine;

namespace Game.Audio
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager singleton;
        AudioSource _audioSource;

        public AudioClip AudioMusic => _audioSource.clip;

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

        public void UpdateMusic(IAudioClipContainer scene)
        {
            if (scene.PlayOnAwake)
                singleton.PlayForBegenning(scene.AudioClip);
            else
                singleton.Play(scene.AudioClip);
        }
    }
}