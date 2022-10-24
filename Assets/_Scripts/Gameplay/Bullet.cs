using System;
using UnityEngine;

namespace Game.Gameplay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float _speed = 2;
        [SerializeField, Range(0f, 10f)] float _timeToDisable = 3f;
        [SerializeField] TrailRenderer _trail;
        float _currentSpeed = 0;
        float _currentSeconds = 0;
        bool _isMoving = false;

        public void Shoot(Vector3 direction)
        {
            transform.forward = direction;
            _currentSpeed = _speed;
            _isMoving = true;
        }

        void Update()
        {
            if (!_isMoving) return;

            if (_currentSeconds >= _timeToDisable)
            {
                SetTrail(false);
                gameObject.SetActive(false);
            }
            else
            {
                Move();
                _currentSeconds += Time.deltaTime;
            }
        }

        void OnDisable()
        {
            float _currentSpeed = 0;
            float _currentSeconds = 0;
            SetTrail(false);
            _isMoving = false;
        }

        void Move()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        void SetTrail(bool enable)
        {
            if (_trail == null) return;
            _trail.enabled = enable;
            _trail.emitting = enable;
        }
    }
}