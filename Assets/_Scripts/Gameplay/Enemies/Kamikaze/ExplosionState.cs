using System;
using System.Collections;
using System.Collections.Generic;
using Game.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class ExplosionState : State
    {
        KamikazeStateController _controller;
        NavMeshAgent agent;
        AnimatorController _ani;
        float _explosionRadius;
        LayerMask _playerLayer;
        int _damage;

        public ExplosionState(KamikazeStateController controller, NavMeshAgent agent, AnimatorController ani, float explosionRadius, LayerMask playerLayer, int damage)
        {
            _controller = controller;
            this.agent = agent;
            _ani = ani;
            _explosionRadius = explosionRadius;
            _playerLayer = playerLayer;
            _damage = damage;
        }

        public override void Enter()
        {
            PlayManager.Instance.SetPlayerControlActive(false, true);
            _ani.AddAnimationEvent("explosion_event", EXPLOSION_EVENT);
            agent.isStopped = true;
            agent.speed = 0;
            _ani.AnticipationExplode();
        }

        private void EXPLOSION_EVENT()
        {
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
