using Game.Gameplay.Player;
using Game.Managers;
using Game.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class HealthUIController : MonoBehaviour
    {
        [SerializeField] Slider _slider;
        [SerializeField] Image _sliderFillImage;
        [SerializeField] Gradient _colorVariention;
        [SerializeField] Animator _effectAnimator;

        [Header("Player's Life")] [SerializeField]
        IntSO _maxPlayerLife;

        [SerializeField] IntSO _actualPlayerLife;

        void Start()
        {
            SetSliderValue();

            GameManager.Player.GetComponent<PlayerDamageable>().OnTakeDamage += PlayDamageEffect;
        }

        void Update()
        {
            SetSliderValue();
        }

        public void SetSliderValue()
        {
            float actualValue = _actualPlayerLife.value;
            float maxValue = _maxPlayerLife.value;

            var value = actualValue / maxValue;
            _slider.value = value;
            _sliderFillImage.color = _colorVariention.Evaluate(value);
        }

        public void PlayDamageEffect(int x, GameObject damaging)
        {
            _effectAnimator.SetTrigger("Play");
        }
    }
}