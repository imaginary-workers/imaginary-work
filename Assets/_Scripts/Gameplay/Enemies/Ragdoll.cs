using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        Collider[] _colliders;
        Rigidbody[] _rigidbodies;
        [SerializeField] Rigidbody _chest;
        [SerializeField] float _knockbackForce = 100;

        void Start()
        {
            _rigidbodies = transform.GetComponentsInChildren<Rigidbody>();
            _colliders = transform.GetComponentsInChildren<Collider>();
            SetEnabled(false);
        }

        public void SetEnabled(bool enabled)
        {
            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = !enabled;
                rigidbody.useGravity = enabled;
                rigidbody.collisionDetectionMode =
                    enabled ? CollisionDetectionMode.Continuous : CollisionDetectionMode.Discrete;
            }

            foreach (var collider in _colliders) collider.enabled = enabled;

            _animator.enabled = !enabled;
        }

        public void Knockback(Vector3 direction)
        {
            _chest.AddForce(direction.normalized * _knockbackForce, ForceMode.Impulse);
        }
    }
}