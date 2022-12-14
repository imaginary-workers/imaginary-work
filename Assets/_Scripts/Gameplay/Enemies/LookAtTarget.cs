using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField] bool _targetAlwaysPlayer;

        [SerializeField] GameObject _target;
        [SerializeField] [Range(0f, 10f)] float _speedRotation = 10f;
        [SerializeField] bool _ignoreX, _ignoreY, _ignoreZ;

        public GameObject Target
        {
            get => _target;
            set => _target = value;
        }

        public float SpeedRotation
        {
            set => _speedRotation = value;
        }

        void Start()
        {
            if (_targetAlwaysPlayer)
                _target = GameManager.Player;
        }

        void Update()
        {
            var targetDirection = _target.transform.position - transform.position;
            targetDirection = new Vector3(
                _ignoreX ? 0 : targetDirection.x,
                _ignoreY ? 0 : targetDirection.y,
                _ignoreZ ? 0 : targetDirection.z
            );
            transform.forward = Vector3.Slerp(transform.forward, targetDirection, Time.deltaTime * _speedRotation);
        }
    }
}