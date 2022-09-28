using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        [SerializeField] GameObject _player;
        [SerializeField] GameObject _pauseMenu;

        bool _isPaused = false;

        void Awake()
        {
            instance = this;
        }

        public static GameObject Player => instance._player;

        public void NewGame()
        {
            SceneManager.LoadScene("Level0");
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
            if (_isPaused)
                Resume();
            else
                Pause();
        }

        public void Pause()
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void Resume()
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        public void RestartLevel()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void BackToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}