using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class ExplosionState : State
    {
        KamikazeStateController _controller;
        NavMeshAgent agent;
        AnimatorController _ani;

        public ExplosionState(KamikazeStateController controller, NavMeshAgent agent, AnimatorController ani)
        {
            _controller = controller;
            this.agent = agent;
            _ani = ani;
        }

        public override void Enter()
        {
            Debug.Log("Explo");
            _ani.AddAnimationEvent("explosion_event", EXPLOSION_EVENT);
            agent.isStopped = true;
            agent.speed = 0;
            _ani.AnticipationExplode();
        }

        private void EXPLOSION_EVENT()
        {
            _controller.Explode();
            _controller.ChangeState(_controller.Dead);
        }

        public override void Update()
        {
          
        }
        public override void Exit()
        {
            _ani.RemoveAnimationEvent("explosion_event");
        }
    }
}
