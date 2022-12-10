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
        VisualField _field;
        VisualField _fieldExplosion;

        public FollowState(KamikazeStateController controller, NavMeshAgent navMeshAgent, VisualField field, VisualField fieldExplosion)
        {
            _controller = controller;
            _navMeshAgent = navMeshAgent;
            _field = field;
            _fieldExplosion = fieldExplosion;
        }

        public override void Enter()
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = _controller.NormalSpeed;
        }
        public override void Update()
        {
            if (!_field.IsTargetInView)
            {
                _controller.ChangeState(_controller.Idle);
            }
            else if (_fieldExplosion.IsTargetInView)
            {
                _controller.ChangeState(_controller.Explosion);
            }
            _navMeshAgent.SetDestination(_controller.Target.transform.position);
        }
        public override void Exit()
        {

        }
    }
}
