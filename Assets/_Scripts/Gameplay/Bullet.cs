using UnityEngine;

namespace Game.Gameplay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float _speed = 20;
        [SerializeField, Range(0f, 10f)] float _timeToDisable = 3f;
        [SerializeField] TrailRenderer _trail;
        float _currentSeconds = 0;
        bool _isMoving = false;
        Vector3 _direction;

        public void Shoot(Vector3 direction)
        {
            _direction = direction;
            _isMoving = true;
        }

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
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 0 || other.gameObject.layer == 6)
            {
                Debug.Log("holis");
                DesactiveBullet();
            }
        }
    }
}