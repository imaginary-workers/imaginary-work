using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class RandomPatrol : MonoBehaviour
    {
        [SerializeField] List<Transform> positions;
        [SerializeField] MoveComponent moveComponent;
        [SerializeField, Range(0, 5)] float _speed = 3;
        [SerializeField] float _distance = 1;
        int _indexRandom = 0;

        void Update()
        {
            Vector3 target = positions[_indexRandom].position;

            var targetDirection = target - transform.position;
            if (Vector3.Distance(transform.position, positions[_indexRandom].position) >= _distance)
            {
                moveComponent.Velocity = targetDirection.normalized * _speed;
            }
            else
            {
                moveComponent.Velocity = Vector3.zero;



                NextIndex();
            }

            targetDirection.y = 0;
            transform.forward = targetDirection;
        }

        public float Speed
        {
            set => _speed = value;
        }

        void NextIndex()
        {
            var newIndex = 0;
            do
            {
                newIndex = Random.Range(0, positions.Count);

            }
            while (newIndex == _indexRandom);
            _indexRandom = newIndex;
        }
    }
}
