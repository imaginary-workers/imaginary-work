using System;
using Game.Config;
using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "gameplay-config", menuName = "Scriptable Object/config/gameplay", order = 0)]
    public class GameplaySettingsSO : ScriptableObject
    {
        [SerializeField] PlayerConfig _playerConfig;

        public event Action OnChange;

        public PlayerConfig PlayerConfig
        {
            get => new PlayerConfig(_playerConfig);
            private set { _playerConfig = new PlayerConfig(value); }
        }

        public void ChangePlayerConfig(PlayerConfig playerConfig)
        {
            PlayerConfig = playerConfig;
            OnChange?.Invoke();
        }
    }
}