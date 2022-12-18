using System;
using Game.Gameplay.Enemies;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Managers
{
    public class GameplayUIManager : MonoBehaviour
    {
        static GameplayUIManager _instance;
        public static GameplayUIManager Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<GameplayUIManager>();
                return _instance;
            }
        }

        [Header("Dependencies")]
        [SerializeField] GameManager _gameManager;
        [Header("Menus")]
        [SerializeField] GameObject _pauseMenu;
        [SerializeField] SkillBarController _barController;
        [Header("HUD Objets")]
        [SerializeField] Text _bulletCounterText;
        [SerializeField] Text _reserveCounterText;
        [SerializeField] Text _countEnemyText;
        [Header("Option Menu")]
        [SerializeField] GameObject _optionsMenu;
        bool _isPaused;
        
        public bool CanPause { get; set; } = true;

        void Awake()
        {
            _instance = this;
            Enemy.UpdateEnemyCount += UpdateEnemyCount;
            
        }

        private void Start()
        {
            UpdateEnemyCount();
        }

        private void OnDestroy()
        {
            Enemy.UpdateEnemyCount -= UpdateEnemyCount;
        }

        public void PauseKeybord()
        {
            if (_gameManager.IsDeath) return;
            if (!CanPause) return;
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
            PlayManager.Instance.CanvasController(true);
        }

        public void Resume()
        {
            _isPaused = false;
            _optionsMenu.GetComponent<OptionMenuUI>().CancelOptions();
            _pauseMenu.SetActive(false);
            _pointer.SetActive(true);
            PlayManager.Instance.CanvasController(false);
        }

        public void OpenOptions()
        {
            _optionsMenu.SetActive(true);
        }

        public void UpdateBulletCounter(int amunicion)
        {
            if (amunicion < 0)
                _bulletCounterText.text = "";
            else
                _bulletCounterText.text = amunicion.ToString();
        }

        public void UpdateEnergyBar(int value, int maxValue)
        {
            _barController.UpdateSkillBar(value, maxValue);
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
            _countEnemyText.text = Enemy.CountEnemy.ToString();
        }

        public void SetActiveSlot(int slot)
        {
            _inventoryUI.SetSlotColorActive(slot);
        }

        public void UnlockedWeaponUI(int slot)
        {
            _inventoryUI.SetUnlokedIcon(slot, false);
        }
    }
}