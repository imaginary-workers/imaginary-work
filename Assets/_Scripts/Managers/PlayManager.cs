using Game.Gameplay.Player;
using UnityEngine;

namespace Game.Managers
{
    public class PlayManager : MonoBehaviour
    {
        GameObject _player;

        static PlayManager _instance;

        public static PlayManager Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<PlayManager>();
                return _instance;
            }
        }
        private void Awake()
        {
            _instance = this;
        }
        private void Start()
        {
            _player = GameManager.Player;
        }
        public void SetPlayerControlActive(bool active)
        {
            _player.GetComponent<PlayerController>().active = active;
            _player.GetComponent<WeaponController>().active = active;
        }
        public void CanvasController(bool active, bool timeScale = true)
        {
            SetPlayerControlActive(!active);
            Cursor.lockState = active ? CursorLockMode.Confined : CursorLockMode.Locked;
            if (!timeScale) return;
            Time.timeScale = active ? 0 : 1;
        }
    }
}
