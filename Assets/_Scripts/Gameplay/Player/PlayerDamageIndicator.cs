using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Player
{
    public class PlayerDamageIndicator : MonoBehaviour
    {
        [SerializeField] PlayerDamageable _damageable;
        [SerializeField] Image _imageDamage;
        [SerializeField] float _timeToDisappear = 3f;
        private float _time = float.MaxValue;
        private bool _onScreen = false;

        void OnEnable()
        {
            _imageDamage.enabled = false;
            _damageable.OnTakeDamage += TakeDamageHandler;
        }

        void OnDisable()
        {
            _damageable.OnTakeDamage -= TakeDamageHandler;
        }

        void Update()
        {
            if (_time < _timeToDisappear)
            {
                _time += 1 * Time.deltaTime;
            }
            else if (_onScreen)
            {
                _imageDamage.enabled = false;
                _onScreen = false;
            }
        }

        void TakeDamageHandler(int damage, GameObject damaginGameObject)
        {
            _imageDamage.enabled = true;
            var playerTransform = _damageable.transform;
            var playerPosition = playerTransform.position;
            var damagingPosition = damaginGameObject.transform.position;
            var playerForward = playerTransform.forward;
            var damagingDirection = (damagingPosition - playerPosition).normalized;
            playerForward.y = damagingDirection.y = 0;
            var angle = Vector3.Angle(playerForward, damagingDirection);
            var rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;
            _time = 0;
            _onScreen = true;
        }
    }
}