using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class HitMarkerController : MonoBehaviour
    {
        [SerializeField] Color _weakColor;
        [SerializeField] Color _strongColor;
        [SerializeField] Color _deathColor;
        [SerializeField] Image _myImage;
        [SerializeField, Range(0.01f,1f)] float _duration;

        //public static HitMarkerController hitMarker;

        //private void Awake()
        //{
        //    hitMarker = this;
        //}

        //Para probarlo
        public bool tryOut;
        private void Update()
        {
            if (tryOut)
            {
                tryOut = false;
                DisplayHitMark(false, true);
            }
        }

        public void DisplayHitMark(bool isDeath, bool isStrong)
        {
            StopAllCoroutines();

            if (isDeath)
                _myImage.color = _deathColor;
            else if (isStrong)
                _myImage.color = _strongColor;
            else
                _myImage.color = _weakColor;

            StartCoroutine("CO_HitMarkVanish");
        }

        IEnumerator CO_HitMarkVanish()
        {
            float waitTime = 0.01f / _duration;

            do {
                Color c = _myImage.color;

                c.a -= waitTime;

                _myImage.color = c;

                yield return new WaitForSeconds(waitTime);

            } while (_myImage.color.a > 0);
        }
    }
}
