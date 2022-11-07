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
using Game.Audio;

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
        [SerializeField] GameObject _optionsMenu;
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

        [Header("Audio")]
        [SerializeField] AudioMixer _audioMixer;


        [Header("Scenes")]
        [SerializeField] SceneStorageSO _sceneStorage;

        [Header("Settings")]
        [SerializeField] GameplaySettingsSO _gameplaySettings;
        [SerializeField] State _state;
        PlayerConfig _newPlayerConfig = null;
        AudioConfig _newAudioConfig = null;
        bool _isPaused = false;
        bool _isDeath = false;
        bool _isChangingScene = false;
        bool _options = false;

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

            MusicManager.singleton.UpdateMusic(_sceneStorage.FindSceneByName(SceneManager.GetActiveScene().name));
        }
        void Update()
        {
            if (Enemy.countEnemy <= 0 && _state == State.Gameplay)
            {
                ConditionWin();
            }
        }

        void OnDestroy()
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

#region Game_FLOW

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
            var sceneSO = _sceneStorage.FindSceneByName("Level0");
            StartCoroutine(CO_NextScene(sceneSO));
        }

        public void ControlsMenu()
        {
            var sceneSO = _sceneStorage.FindSceneByName("ControlsMenu");
            StartCoroutine(CO_NextScene(sceneSO));
        }

        public void RestartLevel()
        {
            var sceneSO = _sceneStorage.FindSceneByName(SceneManager.GetActiveScene().name);
            StartCoroutine(CO_NextScene(sceneSO));
        }

        public void BackToMainMenu()
        {
            var sceneSO = _sceneStorage.FindSceneByName("MainMenu");
            StartCoroutine(CO_NextScene(sceneSO));
        }

        public void ConditionWin()
        {
            SceneSO sceneSO;
            if (SceneManager.GetActiveScene().name == "Level0")
            {
                sceneSO = _sceneStorage.FindSceneByName("Level1");
                StartCoroutine(CO_NextScene(sceneSO));
            }
            else
            {
                sceneSO = _sceneStorage.FindSceneByName("VictoryScreen");
                StartCoroutine(CO_NextScene(sceneSO));
            }
        }

        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

#endregion

#region GAMEPLAY_UI

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
            _isPaused = false;
            _pauseMenu.SetActive(false);
            _pointer.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            _player.GetComponent<PlayerController>().enabled = true;
            _player.GetComponent<WeaponController>()._active = true;
        }

        public void OpenOptions()
        {            
            _options = true;
            _optionsMenu.SetActive(true);

        }
        void CloseOptions()
        {
            _options = false;
            _optionsMenu.SetActive(false);
            _pauseMenu.SetActive(true);

        }
        public void CancelOptions()
        {
            PauseMenuSetup();
            CloseOptions();
        }
        public void ConfirmoOptions()
        {
            UpdateConfig();
            CloseOptions();
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

        public void UpdateEnemyCount()
        {
            _countEnemyText.text = Enemy.countEnemy.ToString();
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

#endregion

#region Config


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
                UpdateAudioMixer();
            }
        }

#endregion

        IEnumerator CO_NextScene(SceneSO scene)
        {
            if (!_isChangingScene)
            {
                _isChangingScene = true;
                _blackScreenAnimator?.SetTrigger("Play");

                yield return new WaitForSecondsRealtime(1f);

                _health.value = _maxHealth.value;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1;
                MusicManager.singleton.UpdateMusic(scene);
                SceneManager.LoadScene(scene.SceneName);
            }
        }
        #region AUDIO
        void UpdateAudioMixer()
        {
            var audioConfig = _gameplaySettings.AudioConfig;
            _audioMixer.SetFloat("Master", audioConfig.Master);
            _audioMixer.SetFloat("Music", audioConfig.Music);
            _audioMixer.SetFloat("Sound", audioConfig.Sound);
        }
#endregion
    }
}
