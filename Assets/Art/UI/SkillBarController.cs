using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{

    public class SkillBarController : MonoBehaviour
    {
        [SerializeField] GameObject _effect;
        [SerializeField] GameObject _icon;
        [SerializeField] Image _bar;

        // Start is called before the first frame update
        void Start()
        {
            UpdateSkillBar(0f, 1f);
        }

        public void UpdateSkillBar(float value, float maxValue)
        {
            _bar.fillAmount = value / maxValue;

            _effect.SetActive(value == maxValue);
            _icon.SetActive(value == maxValue);
        }
    }
}
