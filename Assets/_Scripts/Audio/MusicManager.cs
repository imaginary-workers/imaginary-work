using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager _singleton;
        AudioSource _audioSource;

        public AudioClip AudioMusic { get => _audioSource.clip; }
        void Awake()
        {
            if (_singleton != null && _singleton != this)
            {
                Destroy(gameObject);
            }
            else if (_singleton == null)
            {
                _singleton = this;
                DontDestroyOnLoad(gameObject);
                _audioSource = GetComponent<AudioSource>();
            }
        }
        public void StartMusic(AudioClip music)
        {
            _audioSource.clip = music;
            _audioSource.Play();
        }
        public void StartSounds(AudioClip Sound)
        {
            _audioSource.clip = Sound;
            _audioSource.Play();
        }
    }
}
