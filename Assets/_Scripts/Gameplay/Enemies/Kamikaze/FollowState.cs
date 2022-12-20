using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class FollowState : State
    {
        KamikazeStateController _controller;
        NavMeshAgent _navMeshAgent;
        FollowPlayer _followPlayer;
        GameObject _target;
        float _rangeOfVisionY;
        float _rangeExplosion;
        float _rangeFollow;

        public FollowState(KamikazeStateController controller, NavMeshAgent navMeshAgent, FollowPlayer followPlayer)
        {
            _controller = controller;
            _navMeshAgent = navMeshAgent;
            _target = _controller.Target;
            _followPlayer = followPlayer;
            _rangeExplosion = _controller.RangeExplosion;
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
            _followPlayer.enabled = true;
        }
        public override void Update()
        {
            var position = _controller.transform.position;
            var playerPosition = _target.transform.position;
            if (!Utils.IsInRangeOfVision(position, playerPosition, _rangeFollow, _rangeOfVisionY))
                _controller.ChangeState(_controller.Idle);
            else if (Utils.IsInRangeOfVision(position, playerPosition, _rangeExplosion, _rangeOfVisionY))
                _controller.ChangeState(_controller.Explosion);
        }
        public override void Exit()
        {

        }
    }
}
