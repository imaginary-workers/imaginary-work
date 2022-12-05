using Game.Config;
using Game.Config.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Managers
{
    public class GameplayConfigManager : MonoBehaviour
    {
        [SerializeField] GameplaySettingsSO _gameplaySettings;
        [SerializeField] Slider _speedRotatioSlider;
        [SerializeField] Toggle _invertedXToggle;
        [SerializeField] Toggle _invertedYToggle;
        PlayerConfig _newPlayerConfig;

        PlayerConfig NewPlayerConfig
        {
            get
            {
                if (_newPlayerConfig == null)
                    _newPlayerConfig = _gameplaySettings.PlayerConfig;

                return _newPlayerConfig;
            }
        }

        void OnEnable()
        {
            SetToCurrentConfig();
        }

        public void ConfirmChange()
        {
            if (_newPlayerConfig != null)
            {
                _gameplaySettings.ChangePlayerConfig(_newPlayerConfig);
                _newPlayerConfig = null;
            }
        }

        public void CancelChange()
        {
            SetToCurrentConfig();
        }

        public void SetInvertedYAxis(bool to)
        {
            NewPlayerConfig.invertedYAxis = to;
        }

        public void SetInvertedXAxis(bool to)
        {
            NewPlayerConfig.invertedXAxis = to;
        }

        public void ChangedRotationSpeedValue(float value)
        {
            NewPlayerConfig.rotationSpeed = value;
        }

        void SetToCurrentConfig()
        {
            var config = _gameplaySettings.PlayerConfig;
            _invertedXToggle.isOn = config.invertedXAxis;
            _invertedYToggle.isOn = config.invertedYAxis;
            _speedRotatioSlider.value = config.rotationSpeed;
            _newPlayerConfig = null;
        }
    }
}