using Game.Config;
using Game.Gameplay.Weapon;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.SO;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("Player Info")]
        [SerializeField] GameObject _player;
        [SerializeField] IntSO _maxHealth;
        [SerializeField] IntSO _health;

        [Header("HUD Objets")]
        [SerializeField] GameObject _pauseMenu;
        [SerializeField] GameObject _deathMessege;
        [SerializeField] GameObject _pointer;
        [SerializeField] Text _bulletCounterText;
        [SerializeField] Text _reserveCounterText;
        [SerializeField] Toggle _invertedXToggle;
        [SerializeField] Toggle _invertedYToggle;
        [SerializeField] Slider _speedRotatioSlider;

        [Header("Settings")]
        [SerializeField] GameplaySettingsSO _gameplaySettings;
        PlayerConfig _newConfig = null;
        bool _isPaused = false;
        bool _isDeath = false;

        void Awake()
        {
            instance = this;

            PauseMenuSetup();

            if (_deathMessege != null)
                _deathMessege.SetActive(false);
        }

        public static GameObject Player => instance._player;

        public void DeathScreen()
        {
            _isDeath = true; 
            Cursor.lockState = CursorLockMode.None;
            _deathMessege.SetActive(true);
            _pointer.SetActive(false);
            Time.timeScale = 0;
        }

        public void NewGame()
        {
            _health.value = _maxHealth.value;
            SceneManager.LoadScene("Level0");
        }

        public void ToLevelOne()
        {
            _health.value = _maxHealth.value;
            SceneManager.LoadScene("Level1");
        }

        public void ControlsMenu()
        {
            SceneManager.LoadScene("ControlsMenu");
        }
        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        public void PauseKeybord()
        {
            if (_isDeath) return;

            if (_isPaused)
                Resume();
            else
                Pause();
        }

        public void Pause()
        {
            _isPaused = true;
            _pauseMenu.SetActive(true);
            _pointer.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

        public void Resume()
        {
            UpdateConfig();
            _isPaused = false;
            _pauseMenu.SetActive(false);
            _pointer.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }

        public void RestartLevel()
        {
            Time.timeScale = 1;
            _health.value = _maxHealth.value;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void BackToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }

        public void UpdateBulletCounter(int amunicion)
        {
            if (amunicion < 0)
                _bulletCounterText.text = "";
            else
                _bulletCounterText.text = amunicion.ToString();
        }
        public void UpdateReserveCounter(int amunicion)
        {
            if (amunicion < 0)
                _reserveCounterText.text = "-";
            else
                _reserveCounterText.text = amunicion.ToString();
        }

        public void SetInvertedYAxis(bool to)
        {
            GetNewConfig().invertedYAxis = to;
        }
        
        public void SetInvertedXAxis(bool to)
        {
            GetNewConfig().invertedXAxis = to;
        }

        public void ChangedRotationSpeedValue()
        {
            GetNewConfig().rotationSpeed = _speedRotatioSlider.value;
        }

        void PauseMenuSetup()
        {
            if (_pauseMenu == null) return;
            
            var config = _gameplaySettings.PlayerConfig;
            _invertedXToggle.isOn = config.invertedXAxis;
            _invertedYToggle.isOn = config.invertedYAxis;
            _speedRotatioSlider.value = config.rotationSpeed;
            _pauseMenu.SetActive(false);
        }

        PlayerConfig GetNewConfig()
        {
            if (_newConfig == null)
                _newConfig = _gameplaySettings.PlayerConfig;

            return _newConfig;
        }

        void UpdateConfig()
        {
            if (_newConfig == null) return;
            
            _gameplaySettings.ChangePlayerConfig(_newConfig);
            _newConfig = null;
        }
    }
}
