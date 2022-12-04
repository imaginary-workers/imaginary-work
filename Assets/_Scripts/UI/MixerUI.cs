using Game.Audio;
using Game.Config;
using Game.Decorator;
using Game.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MixerUI : MonoBehaviour
    {
        [SerializeField] GameplaySettingsSO _gameplaySettings;
        [SerializeField] Slider _musicSlider;
        [SerializeField] Slider _soundSlider;
        [SerializeField] Slider _masterAudioSlider;
        AudioConfig _newAudioConfig = null;

        void Awake()
        {
            SetToCurrentConfig();
        }

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
            MixerManager.singleton.UpdateAudioMixer(NewAudioConfig);
        }

        public void ChangedSoundValue(float value)
        {
            var uiAudioDecorator = new AudioConfig01Decorator(NewAudioConfig);
            uiAudioDecorator.Sound = value;
            MixerManager.singleton.UpdateAudioMixer(NewAudioConfig);
        }

        public void ChangedMusicValue(float value)
        {
            var uiAudioDecorator = new AudioConfig01Decorator(NewAudioConfig);
            uiAudioDecorator.Music = value;
            MixerManager.singleton.UpdateAudioMixer(NewAudioConfig);
        }

        void SetToCurrentConfig()
        {
            var audioConfig = _gameplaySettings.AudioConfig;
            MixerManager.singleton.UpdateAudioMixer(audioConfig);
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
    }
}
