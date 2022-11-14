using Game.Gameplay.Enemies.FollowMelee;
using System;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.CollectableEnemy
{
    public class DeadState : AbstractDeadState
    {
        NavMeshAgent _agent;
        MinionEnemy _stateController;
        AnimatorController _animatorcontroler;

        public DeadState(
            NavMeshAgent agent,
            MinionEnemy stateController,
            Action hitStop,
            AnimatorController animatorController
        ) : base(hitStop)
        {
            _agent = agent;
            _stateController = stateController;
            _animatorcontroler = animatorController;
        }

        public override void Enter()
        {
            _agent.speed = 0;
            _agent.isStopped = true;
            _animatorcontroler.enabled = false;
            hitStop();
        }
    }
}