using System.Collections;
using Game.Audio;
using Game.Gameplay.Enemies;
using Game.Gameplay.Lifts;
using Game.Gameplay.Player;
using Game.Scene.SO;
using Game.SO;
using Game.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public enum State
        {
            Menu,
            Gameplay
        }

        const float _secondsToDisplayDeathScreenInSeconds = 3f;
        static GameManager _instance;

        [Header("Player Info")]
        [SerializeField]
        GameObject _player;

        [SerializeField] IntSO _maxHealth;
        [SerializeField] IntSO _health;

        

        [SerializeField] GameObject _deathMessege;

        [Header("GameCanvas Element")]
        [SerializeField]
        GameObject _pointer;
        
        [SerializeField] InventoryUIController _inventoryUI;

        
        [SerializeField] GameObject _controlsMenu;

        [Header("BlackScreen Transition")]
        [SerializeField]
        Animator _blackScreenAnimator;

        [Header("Scenes")][SerializeField] SceneStorageSO _sceneStorage;

        [Header("Audio")][SerializeField] AudioSource _audioSource;

        [SerializeField] AudioClip _gameOver;

        [Header("Settings")][SerializeField] State _state;

        bool _isChangingScene;

        public bool IsDeath { get; private set; }

        LiftStart _liftStart;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<GameManager>();
                return _instance;
            }
        }

        public static GameObject Player
        {
            get
            {
                if (Instance._player == null) Instance._player = FindObjectOfType<PlayerController>()?.gameObject;
                return Instance._player;
            }
        }


        void Awake()
        {
            _instance = this;

            if (_deathMessege != null)
                _deathMessege.SetActive(false);
            if (_state == State.Gameplay)
            {
                _health.value = _maxHealth.value;
                
                Player.GetComponent<PlayerDamageable>().OnDeath += GameOver;
                _liftStart = FindObjectOfType<LiftStart>();
                PlayManager.Instance.CanvasController(false, false);
                if (_liftStart != null)
                {
                    _liftStart.Lift.OnUpFinished += ResumePlayerControl;
                    PlayManager.Instance.SetPlayerControlActive(false);
                    _liftStart.PlacePlayer(Player);
                    _liftStart.Start();
                }
            }
        }

        void Start()
        {
            MusicManager.singleton.UpdateMusic(_sceneStorage.FindSceneByName(SceneManager.GetActiveScene().name));
            if (_state == State.Gameplay)
            {
                
                var weapons = Player.GetComponent<WeaponInventory>().Weapons;
                var weaponsCount = weapons.Count;
                for (var i = 0; i < weaponsCount; i++)
                    if (weapons[i].IsLocked)
                        _inventoryUI.SetUnlokedIcon(i, true);
                    else
                        _inventoryUI.SetUnlokedIcon(i, false);
            }
        }

        void OnDestroy()
        {
            if (_state == State.Gameplay)
            {
                
                Player.GetComponent<PlayerDamageable>().OnDeath -= GameOver;
                if (_liftStart != null) _liftStart.Lift.OnUpFinished -= ResumePlayerControl;
            }
        }

        IEnumerator CO_NextScene(SceneSO scene)
        {
            if (!_isChangingScene)
            {
                _isChangingScene = true;
                _blackScreenAnimator?.SetTrigger("Play");

                yield return new WaitForSecondsRealtime(1f);

                Enemy.ResetEnemyCount();
                _health.value = _maxHealth.value;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1;
                MusicManager.singleton.UpdateMusic(scene);
                SceneManager.LoadScene(scene.SceneName);
            }
        }

        void ResumePlayerControl()
        {
            PlayManager.Instance.SetPlayerControlActive(true);
        }


        #region Game_FLOW

        public void GameOver(GameObject damaging)
        {
            StartCoroutine(CO_GameOver());
        }

        IEnumerator CO_GameOver()
        {
            IsDeath = true;
            Cursor.lockState = CursorLockMode.None;
            _pointer.SetActive(false);
            Time.timeScale = 0.5f;
            yield return new WaitForSecondsRealtime(_secondsToDisplayDeathScreenInSeconds);
            _deathMessege.SetActive(true);
            _audioSource.PlayOneShot(_gameOver);
            Time.timeScale = 0f;
        }

        public void NewGame()
        {
            var sceneSO = _sceneStorage.FindSceneByName("Tutorial");
            StartCoroutine(CO_NextScene(sceneSO));
        }

        public void ControlsMenu(bool activate)
        {
            //var sceneSO = _sceneStorage.FindSceneByName("ControlsMenu");
            //StartCoroutine(CO_NextScene(sceneSO));
            _controlsMenu.SetActive(activate);
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

        public void NextScene(SceneSO sceneSO)
        {
            StartCoroutine(CO_NextScene(sceneSO));
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
    }
}