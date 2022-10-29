using Game.Config;
using Game.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.SO;
using UnityEngine.UI;
using Game.Gameplay.Enemies;
using System.Collections;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public enum State { Menu, Gameplay }
        static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<GameManager>();
                }
                return _instance;
            }
        }

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
        [SerializeField] Animator _blackScreenAnimator;

        [Header("Settings")]
        [SerializeField] GameplaySettingsSO _gameplaySettings;
        [SerializeField] State _state;
        PlayerConfig _newConfig = null;
        bool _isPaused = false;
        bool _isDeath = false;

        void Awake()
        {
            _instance = this;

            PauseMenuSetup();

            if (_deathMessege != null)
                _deathMessege.SetActive(false);
        }
        private void Update()
        {
            if (Enemy.countEnemy <= 0 && _state == State.Gameplay)
            {
                ConditionWin();
            }
        }
        public static GameObject Player
        {
            get
            {
                if (Instance._player == null)
                {
                    Instance._player = FindObjectOfType<PlayerController>()?.gameObject;
                }
                return Instance._player;
            }
        }

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
            StartCoroutine(CO_NextScene("Level0"));
        }

        public void ControlsMenu()
        {
            StartCoroutine(CO_NextScene("ControlsMenu"));
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
            _player.GetComponent<PlayerController>().enabled = false;
            _player.GetComponent<WeaponController>()._active = false;
            
        }

        public void Resume()
        {
            UpdateConfig();
            _isPaused = false;
            _pauseMenu.SetActive(false);
            _pointer.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            _player.GetComponent<PlayerController>().enabled = true;
            _player.GetComponent<WeaponController>()._active = true;
        }

        public void RestartLevel()
        {
            StartCoroutine(CO_NextScene(SceneManager.GetActiveScene().name));
        }
        public void BackToMainMenu()
        {
            StartCoroutine(CO_NextScene("MainMenu"));
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

        public void ConditionWin()
        {
            if (SceneManager.GetActiveScene().name == "Level0")
                StartCoroutine(CO_NextScene("Level1"));
            else
                StartCoroutine(CO_NextScene("VictoryScreen"));

        }

        IEnumerator CO_NextScene(string sceneName)
        {
            _blackScreenAnimator?.SetTrigger("Play");

            yield return new WaitForSecondsRealtime(1f);

            _health.value = _maxHealth.value;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1;

            SceneManager.LoadScene(sceneName);
        }
    }
}
