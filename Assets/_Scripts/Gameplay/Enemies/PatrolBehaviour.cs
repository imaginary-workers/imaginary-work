using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies
{
    public class PatrolBehaviour : MonoBehaviour
    {
        [SerializeField, Range(.1f, 5f)] float _speed = 2f;
        [SerializeField] float _waitBetweenPointInSeconds = 1f;
        [SerializeField] float _waitOnEnable = 2.5f;
        [SerializeField] NavMeshAgent _agent;

        [Tooltip("The order of the points will be the order in which it is patrolled. ('Has to bi bigger than one')")]
        [SerializeField] private List<GameObject> _waypoints;
        [Tooltip("If is active, it will go through the points in cycles. If is NOT, it will go back and forth.")]
        [SerializeField] bool _cycle = false;

        [Space]
        [SerializeField, Range(0f, 5f)] float _pointRadiusGizmos = 2f;
        [SerializeField] Color _sphereColor = Color.red;
        [SerializeField] Color _lineColor = Color.green;

        Vector3[] _patrol;
        int _target = 0;
        int _direction = 1;
        bool _isWaiting = false;
        WaitForSeconds _waitForSeconds;
        WaitForSeconds _waitForSecondsOnEnable;
        [SerializeField] bool _random;

        public float Speed { set { _speed = value; } } 
        void Awake()
        {
            _patrol = new Vector3[_waypoints.Count];
            for (int i = 0; i < _waypoints.Count; i++)
            {
                var transformPosition = _waypoints[i].transform.position;
                _patrol[i] = new Vector3(transformPosition.x, 0, transformPosition.z);
            }
            transform.position = _waypoints[0].transform.position;
            _waitForSeconds = new WaitForSeconds(_waitBetweenPointInSeconds);
            _waitForSecondsOnEnable = new WaitForSeconds(_waitOnEnable);
        }

        void OnEnable()
        {
            _agent.speed = _speed;
            StartCoroutine(CO_StartWaitingAndUpdateDestination(_waitForSecondsOnEnable));
        }

        void Update()
        {
            if (_isWaiting) return;

            if (_agent.remainingDistance <= 0)
            {
                StartCoroutine(CO_StartWaitingAndUpdateDestination(_waitForSeconds));
            }
        }

        void NextDestination()
        {
            NextTarget();
            _agent.SetDestination(_patrol[_target]);
        }

        void NextTarget()
        {
            if (_random)
            {
                var newIndex = 0;
                do
                {
                    newIndex = Random.Range(0, _patrol.Length);

                }
                while (newIndex == _target);
                _target = newIndex;
                return;
            }            
            if (_cycle)
            {
                if (_direction > 0 && _target == _patrol.Length - 1)
                    _target = -1;
                else if (_direction < 0 && _target == 0)
                    _target = _patrol.Length;
            }
            else
            {
                if ((_target == _patrol.Length - 1 && _direction == 1) || (_target == 0 && _direction == -1))
                    ChangeDirection();
            }
            _target += _direction;
        }

        IEnumerator CO_StartWaitingAndUpdateDestination(WaitForSeconds waitForSeconds)
        {
            var speed = _agent.speed;
            _isWaiting = true;
            _agent.speed = 0;
            NextDestination();
            yield return waitForSeconds;
            _agent.speed = speed;
            _isWaiting = false;
        }

        void ChangeDirection()
        {
            _direction *= -1;
        }
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            for (int i = 0; i < _waypoints.Count; i++)
            {
                var currentPosition = _waypoints[i].transform.position;
                Gizmos.color = _sphereColor;
                Gizmos.DrawSphere(currentPosition, _pointRadiusGizmos);
                var nextIdex = i == _waypoints.Count - 1 ? 0 : i + 1;

                if (nextIdex == 0 && !_cycle) return;

                var nextPosition = _waypoints[nextIdex].transform.position;
                Gizmos.color = _lineColor;
                Gizmos.DrawLine(currentPosition, nextPosition);
            }
        }
#endif
    }
}
