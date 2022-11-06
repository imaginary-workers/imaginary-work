using Game.Config;
using Game.Gameplay.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.SO;
using UnityEngine.UI;
using Game.Gameplay.Enemies;
using System.Collections;
using UnityEngine.Audio;

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
        //[SerializeField] AudioMixer _mixer;

        [Header("HUD Objets")]
        [SerializeField] GameObject _pauseMenu;
        [SerializeField] GameObject _deathMessege;
        [SerializeField] GameObject _pointer;
        [SerializeField] Text _bulletCounterText;
        [SerializeField] Text _reserveCounterText;
        [SerializeField] Toggle _invertedXToggle;
        [SerializeField] Toggle _invertedYToggle;
        [SerializeField] Slider _speedRotatioSlider;
        [SerializeField] Slider _masterAudioSlider;
        [SerializeField] Slider _musicSlider;
        [SerializeField] Slider _soundSlider;
        [SerializeField] Animator _blackScreenAnimator;
        [SerializeField] Text _countEnemyText;

        [Header("Settings")]
        [SerializeField] GameplaySettingsSO _gameplaySettings;
        [SerializeField] State _state;
        PlayerConfig _newPlayerConfig = null;
        AudioConfig _newAudioConfig = null;
        bool _isPaused = false;
        bool _isDeath = false;
        bool _isChangingScene = false;

        void Awake()
        {
            
            _instance = this;

            PauseMenuSetup();

            if (_deathMessege != null)
                _deathMessege.SetActive(false);
            if (_state == State.Gameplay)
            {
                Enemy.UpdateEnemyCount += UpdateEnemyCount;
            }
        }
        private void Update()
        {
            if (Enemy.countEnemy <= 0 && _state == State.Gameplay)
            {
                ConditionWin();
            }
        }

        private void OnDestroy()
        {
            if (_state == State.Gameplay)
            {
                Enemy.UpdateEnemyCount -= UpdateEnemyCount;
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
            GetNewPlayerConfig().invertedYAxis = to;
        }

        public void SetInvertedXAxis(bool to)
        {
            GetNewPlayerConfig().invertedXAxis = to;
        }

        public void ChangedRotationSpeedValue()
        {
            GetNewPlayerConfig().rotationSpeed = _speedRotatioSlider.value;
        }

        public void ChangedMasterAudioValue()
        {
            GetNewAudioConfig().Master = _masterAudioSlider.value;
        }

        public void ChangedSoundValue()
        {
            GetNewAudioConfig().Sound = _soundSlider.value;
        }

        public void ChangedMusicValue()
        {
            GetNewAudioConfig().Music = _musicSlider.value;
        }

        void PauseMenuSetup()
        {
            if (_pauseMenu == null) return;

            var config = _gameplaySettings.PlayerConfig;
            _invertedXToggle.isOn = config.invertedXAxis;
            _invertedYToggle.isOn = config.invertedYAxis;
            _speedRotatioSlider.value = config.rotationSpeed;
            var audioConfig = _gameplaySettings.AudioConfig;
            _musicSlider.value = audioConfig.Music;
            _soundSlider.value = audioConfig.Sound;
            _masterAudioSlider.value = audioConfig.Master;
            _pauseMenu.SetActive(false);
        }

        PlayerConfig GetNewPlayerConfig()
        {
            if (_newPlayerConfig == null)
                _newPlayerConfig = _gameplaySettings.PlayerConfig;

            return _newPlayerConfig;
        }

        AudioConfig GetNewAudioConfig()
        {
            if (_newAudioConfig == null)
                _newAudioConfig = _gameplaySettings.AudioConfig;

            return _newAudioConfig;
        }

        void UpdateConfig()
        {
            if (_newPlayerConfig != null)
            {
                _gameplaySettings.ChangePlayerConfig(_newPlayerConfig);
                _newPlayerConfig = null;
            }
            if (_newAudioConfig != null)
            {
                _gameplaySettings.ChangeAudioConfig(_newAudioConfig);
                _newAudioConfig = null;
            }
        }

        public void ConditionWin()
        {
            if (SceneManager.GetActiveScene().name == "Level0")
                StartCoroutine(CO_NextScene("Level1"));
            else
                StartCoroutine(CO_NextScene("VictoryScreen"));

        }

        public void UpdateEnemyCount()
        {
            _countEnemyText.text = Enemy.countEnemy.ToString();
        }

        IEnumerator CO_NextScene(string sceneName)
        {
            if (!_isChangingScene)
            {
                _isChangingScene = true;
                _blackScreenAnimator?.SetTrigger("Play");

                yield return new WaitForSecondsRealtime(1f);

                _health.value = _maxHealth.value;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1;

                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
