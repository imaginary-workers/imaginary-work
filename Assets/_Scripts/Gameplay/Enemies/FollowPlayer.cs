using Game.Gameplay.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField, Range(0f, 5f)] float _speed = 5f;
        [SerializeField] NavMeshAgent _agent;
        [SerializeField, Range(0f, 5f)] float _closeRange = 2f;
        [SerializeField, Range(.1f, 3f)] float _rangeOfVisionY = 1;
        PlayerController _player;
        public float CloseRange => _closeRange;
        public float RangeOfVisionY
        {
            set => _rangeOfVisionY = value;
        }

        public float Speed
        {
            set => _speed = value;
        }

        void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
        }
        void Update()
        {
            var playerPosition = _player.transform.position;
            var currentPosition = transform.position;
            _agent.SetDestination(playerPosition);
            
        }
        private void OnEnable()
        {
            _agent.speed = _speed;
        }
    }
}
