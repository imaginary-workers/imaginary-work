using System;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class JumpComponent : MonoBehaviour
    {
        [Header("Dependencies")] [SerializeField]
        Rigidbody _rigidbody;

        [SerializeField] float _jumpForce = 10f;
        [SerializeField] LayerMask _floorLayer;
        [SerializeField] GameObject _cheeckFloor;
        [SerializeField] [Range(0, 2)] float _checkFloorRadius;
        [SerializeField] float _maxExtraTime = 1;
        bool _alreadyJump;
        float _extraTime;
        public bool IsOnTheFloor { get; private set; } = true;

        void Update()
        {
            CheckIfIsOnTheFloor();
            if (!IsOnTheFloor && !_alreadyJump) _extraTime += 1 * Time.deltaTime;
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_cheeckFloor.transform.position, _checkFloorRadius);
        }

#endif
        public event Action OnJumpEvent;

        public void JumpAction()
        {
            if (_alreadyJump) return;
            if (IsOnTheFloor || (!IsOnTheFloor && _extraTime < _maxExtraTime))
            {
                _alreadyJump = true;
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
                OnJumpEvent?.Invoke();
            }
        }

        void CheckIfIsOnTheFloor()
        {
            if (Physics.OverlapSphere(_cheeckFloor.transform.position, _checkFloorRadius, _floorLayer).Length >= 1)
            {
                if (!IsOnTheFloor)
                {
                    _extraTime = 0;
                }
                _alreadyJump = false;

                IsOnTheFloor = true;
            }
            else if (Physics.OverlapSphere(_cheeckFloor.transform.position, _checkFloorRadius, _floorLayer).Length <= 1)
            {
                IsOnTheFloor = false;
            }
        }
    }
}