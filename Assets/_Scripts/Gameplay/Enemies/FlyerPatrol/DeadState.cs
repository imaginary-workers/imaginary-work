using System;
using Game.Gameplay.Enemies;
using Game.Gameplay.Enemies.FlyerPatrol;
using UnityEngine.AI;

namespace Game._Scripts.Gameplay.Enemies.FlyerPatrol
{
    public class DeadState : AbstractDeadState
    {
        NavMeshAgent _agent;
        FlyerPatrolStateController _stateController;

        public DeadState(
            NavMeshAgent agent,
            FlyerPatrolStateController stateController,
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