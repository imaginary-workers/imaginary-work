using UnityEngine;

namespace Game.Gameplay
{
    public class CameraFollowTarget : MonoBehaviour
    {
        [SerializeField] GameObject _target;

        void Awake()
        {
            transform.forward = _target.transform.forward;
        }

        void LateUpdate()
        {
            transform.position = _target.transform.position;
        }
    }
}