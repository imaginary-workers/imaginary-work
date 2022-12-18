using Game.Gameplay.Player;
using UnityEngine;

namespace Game.Managers
{
    public class PlayManager : MonoBehaviour
    {
        GameObject _player;

        static PlayManager _instance;
        float _speedPlayer;
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
            _player = GameManager.Player;
        }

        public void SetPlayerControlActive(bool active, bool editSpeed = false)
        {
            PlayerController playerController = _player.GetComponent<PlayerController>();
            if (!active && editSpeed)
            {
                playerController.MoveDefault();
            }
            playerController.active = active;
            _player.GetComponent<WeaponController>().active = active;
        }
        public void CanvasController(bool active, bool timeScale = true)
        {
            SetPlayerControlActive(!active, !timeScale);
            SetCursorActive(active);
            if (!timeScale) return;
            Time.timeScale = active ? 0 : 1;
        }

        public void SetCursorActive(bool active)
        {
            Cursor.lockState = active ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
    }
}
