using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Game.Gameplay
{
    public class VisualField : MonoBehaviour
    {
        [SerializeField] GameObject _target;
        [SerializeField] float _visualAngle = 45f;
        [SerializeField] float _visualDistance = 10f;
        [SerializeField] [Range(0f, 30f)] float _rangeOfVisionYUp = 1.5f;
        [SerializeField] [Range(0f, -30f)] float _rangeOfVisionYDown = -1.5f;
        [SerializeField] float _maxTime = .2f;
        [SerializeField] LayerMask _obstacleLayer;
        [SerializeField] LayerMask _targetLayer;
        RaycastHit _hitInfo;
        Ray _ray;
        WaitForSeconds _waitForSeconds;

        public bool IsTargetInView { get; private set; }

        public GameObject Target
        {
            set => _target = value;
        }

        void Start()
        {
            if (_target == null) Debug.LogError("VisualField must have a target");
            _ray = new Ray();
            _hitInfo = new RaycastHit();
            _waitForSeconds = new WaitForSeconds(_maxTime);
            StartCoroutine(CO_FindTarget());
        }


#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            var transformPosition = transform.position;
            var viewAngleLeft = Utils.DirectionFromAngle(transform.eulerAngles.y, -_visualAngle / 2);
            var viewAngleRight = Utils.DirectionFromAngle(transform.eulerAngles.y, _visualAngle / 2);

            transformPosition.y += _rangeOfVisionYDown;
            Handles.color = Color.white;
            Handles.DrawWireArc(transformPosition, Vector3.up, Vector3.forward, 360, _visualDistance);
            Handles.color = Color.yellow;
            Handles.DrawLine(transformPosition, transformPosition + viewAngleLeft * _visualDistance);
            Handles.DrawLine(transformPosition, transformPosition + viewAngleRight * _visualDistance);
            transformPosition.y += _rangeOfVisionYUp - _rangeOfVisionYDown;
            Handles.color = Color.white;
            Handles.DrawWireArc(transformPosition, Vector3.up, Vector3.forward, 360, _visualDistance);
            Handles.color = Color.yellow;
            Handles.DrawLine(transformPosition, transformPosition + viewAngleLeft * _visualDistance);
            Handles.DrawLine(transformPosition, transformPosition + viewAngleRight * _visualDistance);

            if (_target == null) return;
            if (CanSeeTarget())
            {
                Handles.color = Color.red;
                Handles.DrawLine(transform.position, _target.transform.position);
            }
        }
#endif
        public event Action<GameObject> OnEnterViewTarget, OnExitViewTarget;

        IEnumerator CO_FindTarget()
        {
            while (true)
            {
                yield return _waitForSeconds;
                if (CanSeeTarget())
                {
                    if (!IsTargetInView)
                    {
                        IsTargetInView = true;
                        OnEnterViewTarget?.Invoke(_target);
                    }
                }
                else
                {
                    if (IsTargetInView)
                    {
                        IsTargetInView = false;
                        OnExitViewTarget?.Invoke(_target);
                    }
                }
            }
        }

        bool CanSeeTarget()
        {
            var position = transform.position;
            var target = _target.transform;
            if (!IsTargetInRange(position)) return false;
            if (!IsTargetInAngle(position, target.position)) return false;
            if (ThereIsObstacleBetween(position, target.position)) return false;

            return true;
        }

        bool ThereIsObstacleBetween(Vector3 position, Vector3 targetPosition)
        {
            targetPosition.y += 1;
            _ray.origin = position;
            _ray.direction = targetPosition - position;

            var distance = Utils.pythagoreanTheorem(_visualDistance, Mathf.Abs(targetPosition.y - position.y));
            if (Physics.Raycast(_ray, out _hitInfo, distance, _obstacleLayer))
                if (_hitInfo.collider.gameObject != _target)
                    return true;

            return false;
        }

        bool IsTargetInRange(Vector3 position)
        {
            var positionDown = position;
            var positionUp = position;
            positionDown.y += _rangeOfVisionYDown;
            positionUp.y += _rangeOfVisionYUp;
            var targetsInViewRadius = Physics.OverlapCapsule(positionDown, positionUp, _visualDistance, _targetLayer);
            return targetsInViewRadius.Length > 0;
        }

        bool IsTargetInAngle(Vector3 position, Vector3 targetPosition)
        {
            targetPosition.y = position.y = 0;
            return Vector3.Angle(transform.forward, (targetPosition - position).normalized) < _visualAngle / 2;
        }
    }
}