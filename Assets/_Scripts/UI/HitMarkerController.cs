using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class HitMarkerController : MonoBehaviour
    {
        public static HitMarkerController Instance;
        [SerializeField] Color _weakColor;
        [SerializeField] Color _strongColor;
        [SerializeField] Color _deathColor;
        [SerializeField] Image _myImage;
        [SerializeField] [Range(0.01f, 1f)] float _duration;

        void Awake()
        {
            Instance = this;
        }

        public void DisplayHitMarkStrong()
        {
            DisplayHitMark(_strongColor);
        }

        public void DisplayHitMarkWeak()
        {
            DisplayHitMark(_weakColor);
        }

        public void DisplayHitMarkDeath()
        {
            DisplayHitMark(_deathColor);
        }

        void DisplayHitMark(Color color)
        {
            StopAllCoroutines();
            _myImage.color = color;
            StartCoroutine(CO_HitMarkVanish());
        }

        IEnumerator CO_HitMarkVanish()
        {
            var waitTime = 0.01f / _duration;

            do
            {
                var c = _myImage.color;

                c.a -= waitTime;

                _myImage.color = c;

                yield return new WaitForSeconds(waitTime);
            } while (_myImage.color.a > 0);
        }
    }
}