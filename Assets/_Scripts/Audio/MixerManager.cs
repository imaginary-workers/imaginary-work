using Game.Config;
using Game.SO;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Audio
{
    public class MixerManager : MonoBehaviour
    {
        public static MixerManager singleton;
        [SerializeField] GameplaySettingsSO _gameplaySettings;
        [SerializeField] AudioMixer _audioMixer;

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
            }
        }

        void Start()
        {
            UpdateAudioMixer(_gameplaySettings.AudioConfig);
        }

        public void UpdateAudioMixer(AudioConfig newAudioConfig)
        {
            _audioMixer.SetFloat("Master", newAudioConfig.Master);
            _audioMixer.SetFloat("Music", newAudioConfig.Music);
            _audioMixer.SetFloat("Sound", newAudioConfig.Sound);
        }
    }
}