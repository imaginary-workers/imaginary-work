using System;
using UnityEngine;

namespace Game.Gameplay
{
    public class OnPlayerOver : MonoBehaviour
    {
        [SerializeField] string _playerTag = "";
        [SerializeField] BoxCollider _collider;

        [Tooltip("it calculate base on the scale and position. If it grows or move set it to false")] [SerializeField]
        bool _isStaticObject = true;

        float _localScaleX, _localScaleZ, _borderX1, _borderX2, _borderZ1, _borderZ2;

        public bool IsPlayerOver { get; private set; }

        void Awake()
        {
            SetBorders();
        }

        public void Reset()
        {
            IsPlayerOver = false;
        }

        void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(_playerTag)) return;

            if (!_isStaticObject) SetBorders();

            if (!IsOver(other.transform)) return;
            if (!IsInAreaXZ(other.transform)) return;

            IsPlayerOver = true;
            OnPlayerOverEnter?.Invoke(other.gameObject);
        }

        void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.CompareTag(_playerTag)) return;

            IsPlayerOver = false;
            OnPlayerOverExit?.Invoke(other.gameObject);
        }

        public event Action<GameObject> OnPlayerOverEnter, OnPlayerOverExit;

        bool IsInAreaXZ(Transform other)
        {
            var otherPosition = other.position;
            return otherPosition.x > _borderX1 && otherPosition.x < _borderX2 &&
                   otherPosition.z > _borderZ1 && otherPosition.z < _borderZ2;
        }

        bool IsOver(Transform other)
        {
            return other.position.y > transform.position.y;
        }

        void SetBorders()
        {
            var transformPosition = transform.position;
            var size = _collider.size;
            _localScaleX = size.x;
            _localScaleZ = size.z;
            _borderX1 = transformPosition.x - _localScaleX / 2;
            _borderX2 = transformPosition.x + _localScaleX / 2;
            _borderZ1 = transformPosition.z - _localScaleZ / 2;
            _borderZ2 = transformPosition.z + _localScaleZ / 2;
        }
    }
}