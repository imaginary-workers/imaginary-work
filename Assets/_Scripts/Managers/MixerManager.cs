using Game.Config;
using Game.Decorator;
using Game.SO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game.Managers
{
    public class MixerManager : MonoBehaviour
    {
        [SerializeField] GameplaySettingsSO _gameplaySettings;
        [SerializeField] AudioMixer _audioMixer;
        [SerializeField] Slider _musicSlider;
        [SerializeField] Slider _soundSlider;
        [SerializeField] Slider _masterAudioSlider;
        AudioConfig _newAudioConfig = null;

        void OnEnable()
        {
            SetToCurrentConfig();
        }

        public void ConfirmChange()
        {
            if (_newAudioConfig != null)
            {
                _gameplaySettings.ChangeAudioConfig(_newAudioConfig);
                _newAudioConfig = null;
            }
        }

        public void CancelChange()
        {
            SetToCurrentConfig();
        }
        public void ChangedMasterAudioValue(float value)
        {
            var uiAudioDecorator = new AudioConfig01Decorator(NewAudioConfig);
            uiAudioDecorator.Master = value;
            UpdateAudioMixer(NewAudioConfig);
        }

        public void ChangedSoundValue(float value)
        {
            var uiAudioDecorator = new AudioConfig01Decorator(NewAudioConfig);
            uiAudioDecorator.Sound = value;
            UpdateAudioMixer(NewAudioConfig);
        }

        public void ChangedMusicValue(float value)
        {
            var uiAudioDecorator = new AudioConfig01Decorator(NewAudioConfig);
            uiAudioDecorator.Music = value;
            UpdateAudioMixer(NewAudioConfig);
        }

        void SetToCurrentConfig()
        {
            var audioConfig = _gameplaySettings.AudioConfig;
            UpdateAudioMixer(audioConfig);
            var audioUiDecorator = new AudioConfig01Decorator(audioConfig);
            _musicSlider.value = audioUiDecorator.Music;
            _soundSlider.value = audioUiDecorator.Sound;
            _masterAudioSlider.value = audioUiDecorator.Master;
            _newAudioConfig = null;
        }

        AudioConfig NewAudioConfig
        {
            get
            {
                if (_newAudioConfig == null)
                    _newAudioConfig = _gameplaySettings.AudioConfig;

                return _newAudioConfig;
            }
        }
        void UpdateAudioMixer(AudioConfig config)
        {
            _audioMixer.SetFloat("Master", config.Master);
            _audioMixer.SetFloat("Music", config.Music);
            _audioMixer.SetFloat("Sound", config.Sound);
        }
    }
}
