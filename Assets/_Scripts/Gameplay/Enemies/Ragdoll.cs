using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        Rigidbody[] _rigidbodies;
        Collider[] _colliders;

        void Start()
        {
            _rigidbodies = transform.GetComponentsInChildren<Rigidbody>();
            _colliders = transform.GetComponentsInChildren<Collider>();
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
            foreach (Collider collider in _colliders)
            {
                collider.enabled = enabled;
            }

            _animator.enabled = !enabled;
        }
    }
}
