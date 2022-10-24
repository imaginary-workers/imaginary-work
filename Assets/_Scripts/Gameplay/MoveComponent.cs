using UnityEngine;

namespace Game.Gameplay
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;
        [Space]
        [SerializeField] bool _ignoreGravity = false;

        public Vector3 Velocity { get; set; } = Vector3.zero;

        void FixedUpdate()
        {
            _rigidbody.velocity = new Vector3(
                Velocity.x,
                _ignoreGravity? Velocity.y : _rigidbody.velocity.y,
                Velocity.z
                );
        }
    }
}
