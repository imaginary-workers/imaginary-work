using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class VictorySceneCorrutine : MonoBehaviour
    {
        [Header("Background")]
        [SerializeField] Image _backGround;
        [SerializeField, Range(0,1)] float _imageAlpha;

        [Header("Text 1")]
        [SerializeField] TextMeshProUGUI _textOne;
        [SerializeField] string _messageOne;

        [Header("Text 2")]
        [SerializeField] TextMeshProUGUI _textTwo;
        [SerializeField] string _messageTwo;

        [Header("Button")]
        [SerializeField] Button _button;

        [SerializeField, Range(0,0.1f)] float _generalWaitTime;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("VictorySecuence");
        }
            
        IEnumerator VictorySecuence()
        {
            yield return new WaitForSeconds(3.5f);
            var waitTime = new WaitForSeconds(_generalWaitTime);

            do
            {
                var c = _backGround.color;
                c.a += 0.01f;
                _backGround.color = c;
                yield return waitTime;

            } while (_backGround.color.a < _imageAlpha);

            for (int i = 0; i < _messageOne.Length; i++)
            {
                _textOne.text = _messageOne.Substring(0, i+1);
                yield return waitTime;
            }

            for (int i = 0; i < _messageTwo.Length; i++)
            {
                _textTwo.text = _messageTwo.Substring(0, i+1);
                yield return waitTime;
            }

            do
            {
                var s = _button.transform.localScale;
                s.x += 0.02f;
                s.y += 0.02f;
                s.z += 0.02f;
                _button.transform.localScale = s;
                yield return waitTime;

            } while (_button.transform.localScale.x < 1);
        }
    }
}
