using System;
using UnityEngine;

namespace Game.Config.SO
{
    [CreateAssetMenu(fileName = "gameplay-config", menuName = "Scriptable Object/config/gameplay", order = 0)]
    public class GameplaySettingsSO : ScriptableObject
    {
        [SerializeField] PlayerConfig _playerConfig;
        [SerializeField] AudioConfig _audioConfig;

        public PlayerConfig PlayerConfig
        {
            get => new PlayerConfig(_playerConfig);
            private set => _playerConfig = new PlayerConfig(value);
        }

        public AudioConfig AudioConfig
        {
            get => new AudioConfig(_audioConfig);
            private set => _audioConfig = new AudioConfig(value);
        }

        public event Action OnChange;

        public void ChangePlayerConfig(PlayerConfig playerConfig)
        {
            PlayerConfig = playerConfig;
            OnChange?.Invoke();
        }

        public void ChangeAudioConfig(AudioConfig audioConfig)
        {
            AudioConfig = audioConfig;
            OnChange?.Invoke();
        }
    }
}