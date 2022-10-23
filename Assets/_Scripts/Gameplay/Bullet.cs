using System.Collections;
using UnityEngine;

namespace Game.Gameplay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] MoveComponent moveComponent;
        [SerializeField] float _speed = 2;
        [SerializeField, Range(0f, 10f)] float _timeToDisable = 3f;
        [SerializeField] TrailRenderer _trail;

        void OnEnable()
            => moveComponent.enabled = false;

        public void Shoot(Vector3 direction)
        {
            transform.forward = direction;
            moveComponent.enabled = true;
            moveComponent.Velocity = direction * _speed;
            SetTrail(true);
            StartCoroutine(CO_Disable());
        }

        IEnumerator CO_Disable()
        {
            yield return new WaitForSeconds(_timeToDisable);
            SetTrail(false);
            gameObject.SetActive(false);
        }

        void SetTrail(bool enable)
        {
            if (_trail == null) return;
            _trail.enabled = enable;
            _trail.emitting = enable;
        }
    }
}