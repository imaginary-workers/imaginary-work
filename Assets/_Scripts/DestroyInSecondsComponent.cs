using System;
using UnityEngine;

namespace Game
{
    public class DestroyInSecondsComponent : MonoBehaviour
    {
        float _timeToDestroy;
        float _time;
        bool _canCount;
        private Action _callback;

        void OnEnable()
        {
            _timeToDestroy = 0;
            _time = 0;
            _canCount = false;
        }

        void Update()
        {
            if (!_canCount) return;
            if (_time < _timeToDestroy)
            {
                _time += 1 * Time.deltaTime;
            }
            else
            {
                if (_callback != null)
                    _callback();
                gameObject.SetActive(false);
            }
        }

        public void DestroyInSeconds(float seconds)
        {
            _time = 0;
            _timeToDestroy = seconds;
            _canCount = true;
        }
        public void DestroyInSeconds(float seconds, Action callback)
        {
            _time = 0;
            _timeToDestroy = seconds;
            _canCount = true;
            _callback = callback;
        }
    }
}