using Game.Gameplay.Enemies.Boss;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BossHealthUI : MonoBehaviour
    {
        [SerializeField] Slider _healthSlider;
        [SerializeField] BossHealth _bossHealth;

        void Start()
        {
            _healthSlider.maxValue = _bossHealth.MaxHealth;
            _healthSlider.value = _bossHealth.MaxHealth;
            _bossHealth.OnTakeAnyDamage += UpdateSlider;
        }

        private void UpdateSlider()
        {
            _healthSlider.value = _bossHealth.Health;
        }
    }
}
