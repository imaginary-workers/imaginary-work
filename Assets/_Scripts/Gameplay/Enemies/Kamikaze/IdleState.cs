using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class IdleState : State
    {
        KamikazeStateController _controller;
        NavMeshAgent _navMeshAgent;
        private GameObject _target;
        private float _rangeOfVisionY;
        private float _rangeFollow;

        public IdleState(KamikazeStateController controller, NavMeshAgent navMeshAgent)
        {
            _controller = controller;
            _navMeshAgent = navMeshAgent;
            _target = _controller.Target;
            _rangeOfVisionY = _controller.RangeOfVisionY;
            _rangeFollow = _controller.RangeFollow;
        }

        public float RangeFollow
        {
            set
            {
                _rangeFollow = value;
            }
        }
        public override void Enter()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.speed = 0;
        }
        public override void Update()
        {
            var position = _controller.transform.position;
            var playerPosition = _target.transform.position;
            if (Utils.IsInRangeOfVision(position, playerPosition, _rangeFollow, _rangeOfVisionY))
                _controller.ChangeState(_controller.Follow);
        }
        public override void Exit()
        {
           
        }
    }
}
