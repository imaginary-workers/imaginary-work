using System;
using Game.Gameplay.Enemies;
using UnityEngine.AI;

namespace Game._Scripts.Gameplay.Enemies.FlyerPatrol
{
    public class DeadState : AbstractDeadState
    {
        readonly NavMeshAgent _agent;
        readonly Action _deathCallback;

        public DeadState(
            NavMeshAgent agent,
            Action deathCallback
        )
        {
            _agent = agent;
            _deathCallback = deathCallback;
        }

        public override void Enter()
        {
            base.Enter();
            _agent.speed = 0;
            _agent.isStopped = true;
            _deathCallback?.Invoke();
        }
    }
}