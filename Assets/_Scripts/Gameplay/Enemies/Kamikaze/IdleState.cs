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
        VisualField _field;

        public IdleState(KamikazeStateController controller, NavMeshAgent navMeshAgent, VisualField field)
        {
            _controller = controller;
            _navMeshAgent = navMeshAgent;
            _field = field;
        }
        public override void Enter()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.speed = 0;
        }
        public override void Update()
        {
            if (_field.IsTargetInView)
            {
                _controller.ChangeState(_controller.Follow);
            }
        }
        public override void Exit()
        {
           
        }
    }
}
