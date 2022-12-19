using System.Collections;
using Game.Audio;
using Game.Gameplay.Enemies;
using Game.Gameplay.Lifts;
using Game.Gameplay.Player;
using Game.Scene.SO;
using Game.SO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        [Header("BlackScreen Transition")]
        [SerializeField]
        Animator _blackScreenAnimator;

        [Header("Scenes")][SerializeField] SceneStorageSO _sceneStorage;
        [SerializeField] eventSO _deadBossEvent;
        [SerializeField] SceneSO destroyBossScene;

        [Header("Audio")][SerializeField] AudioSource _audioSource;

        [SerializeField] AudioClip _gameOver;

        [Header("Settings")][SerializeField] State _state;

        bool _isChangingScene;

        public bool IsDeath { get; private set; }

        LiftStart _liftStart;
        [SerializeField] GameplayUIManager _gameplayUIManager;

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
            _deadBossEvent?.RegisterEvent(ChangeToDestroyBoss);
            if (_state == State.Gameplay)
            {
                _health.value = _maxHealth.value;
                
                Player.GetComponent<PlayerDamageable>().OnDeath += GameOver;
            }
        }

        void Start()
        {
            MusicManager.singleton.UpdateMusic(_sceneStorage.FindSceneByName(SceneManager.GetActiveScene().name));
        }

        private void ChangeToDestroyBoss()
        {
            Debug.Log("scenemuerte");
            NextScene(destroyBossScene);
        }

        void OnDestroy()
        {
            if (_state == State.Gameplay)
            {
                
                Player.GetComponent<PlayerDamageable>().OnDeath -= GameOver;
                if (_deadBossEvent != null)
                {
                    _deadBossEvent.Unregister(ChangeToDestroyBoss);
                }
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


        #region Game_FLOW

        public void GameOver(GameObject damaging)
        {
            StartCoroutine(CO_GameOver());
        }

        IEnumerator CO_GameOver()
        {
            IsDeath = true;
            Cursor.lockState = CursorLockMode.None;
            _gameplayUIManager.SetPointerActive(false);
            Time.timeScale = 0.5f;
            yield return new WaitForSecondsRealtime(_secondsToDisplayDeathScreenInSeconds);
            _gameplayUIManager.SetDeathMessegeActive(true);
            _audioSource.PlayOneShot(_gameOver);
            Time.timeScale = 0f;
        }

        public void NewGame()
        {
            var sceneSO = _sceneStorage.FindSceneByName("Tutorial");
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