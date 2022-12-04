using UnityEngine;

namespace Game
{
    public class SwayBehaviour : MonoBehaviour
    {
        [SerializeField] Transform _originTarget;
        [SerializeField] float _localZOffset = 2f;

        void Start()
        {
            transform.parent = null;
        }

        void LateUpdate()
        {
            transform.position = _originTarget.position + _originTarget.forward * _localZOffset;
        }
    }
}