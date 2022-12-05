using UnityEngine;

namespace Game.Gameplay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float _speed = 20;
        [SerializeField] [Range(0f, 10f)] float _timeToDisable = 3f;
        [SerializeField] TrailRenderer _trail;
        float _currentSeconds;
        Vector3 _direction;
        bool _isMoving;

        void Update()
        {
            if (!_isMoving) return;

            if (_currentSeconds >= _timeToDisable)
            {
                DesactiveBullet();
            }
            else
            {
                Move();
                _currentSeconds += Time.deltaTime;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 0 || other.gameObject.layer == 6) DesactiveBullet();
        }

        public void Shoot(Vector3 direction)
        {
            _direction = direction;
            _isMoving = true;
        }

        void DesactiveBullet()
        {
            _isMoving = false;
            _currentSeconds = 0;
            SetTrail(false);
            gameObject.SetActive(false);
        }

        void Move()
        {
            transform.position += _direction * _speed * Time.deltaTime;
        }

        void SetTrail(bool enable)
        {
            if (_trail == null) return;
            _trail.enabled = enable;
            _trail.emitting = enable;
        }
    }
}