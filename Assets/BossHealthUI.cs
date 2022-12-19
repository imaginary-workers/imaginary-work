using Game.Gameplay.Enemies.Boss;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BossHealthUI : MonoBehaviour
    {
        [SerializeField] Slider _healthSlider;
        [SerializeField] BossHealth _bossHealth;
        [SerializeField] Animator _animator;

        void Start()
        {
            _healthSlider.maxValue = _bossHealth.MaxHealth;
        }

        void LateUpdate()
        {
            _healthSlider.value = _bossHealth.Health;
        }
    }
}
