using System;
using UnityEditor;
using UnityEngine;

namespace Game.Gameplay
{
    public class VisualField : MonoBehaviour
    {
        public event Action<GameObject> OnEnterViewTarget, OnExitViewTarget, OnStayViewTarget;

        [SerializeField] GameObject _target;
        [SerializeField] float _visualAngle = 45f;
        [SerializeField] float _visualDistance = 10f;
        [SerializeField, Range(.1f, 30f)] float _rangeOfVisionY = 1.5f;
        [SerializeField, Range(0f, 30f)] float _rangeOfVisionYUp = 1.5f;
        [SerializeField, Range(0f, -30f)] float _rangeOfVisionYDown = -1.5f;
        [SerializeField] LayerMask _obstacleLayer;
        [SerializeField] LayerMask _targetLayer;
        bool _isTargetInView = false;

        void Update()
        {
            if (CanSeeTarget())
            {
                if (!_isTargetInView)
                {
                    OnEnterViewTarget?.Invoke(_target);
                    _isTargetInView = true;
                }
            }
            else
            {
                if (_isTargetInView)
                {
                    OnExitViewTarget?.Invoke(_target);
                    _isTargetInView = false;
                }
            }
        }

        public bool IsTargetInView => _isTargetInView;
        public GameObject Target { set => _target = value; }
        
        bool CanSeeTarget()
        {
            var position = transform.position;
            var positionDown = position;
            var positionUp = position;
            positionDown.y += _rangeOfVisionYDown;
            positionUp.y += _rangeOfVisionYUp;
            var targetsInViewRadius = Physics.OverlapCapsule(positionDown, positionUp, _visualDistance, _targetLayer);
            foreach (var collider in targetsInViewRadius)
            {
                if (collider.gameObject != _target)
                    continue;
                var target = collider.transform;
                var targetPlane = target.position;
                targetPlane.y = position.y;
                if (Vector3.Angle (transform.forward, (targetPlane - position).normalized) < _visualAngle / 2) {
                    float distanceToTarget = Vector3.Distance(position, target.position);

                    if (!Physics.Raycast(position, (target.position - position).normalized, distanceToTarget, _obstacleLayer))
                    {
                        return true;
                    }
                }
                
            }

            return false;
            
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

            if (CanSeeTarget())
            {
                Handles.color = Color.red;
                Handles.DrawLine(transform.position, _target.transform.position);
            }
        }
        #endif
    }
}
