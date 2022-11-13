using System;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.CollectableEnemy
{
    public class DeadState : AbstractDeadState
    {
        NavMeshAgent _agent;
        MinionEnemy _stateController;

        public DeadState(
            NavMeshAgent agent,
            MinionEnemy stateController,
            Action hitStop
        ) : base(hitStop)
        {
            _agent = agent;
            _stateController = stateController;
        }

        public override void Enter()
        {
            _agent.speed = 0;
            _agent.isStopped = true;
            hitStop();
        }
    }
}