using Game.Gameplay.Enemies.FollowMelee;
using System;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.CollectableEnemy
{
    public class DeadState : AbstractDeadState
    {
        NavMeshAgent _agent;     
        AnimatorController _animatorcontroler;
        SpawnDrops _spawnDrops;

        public DeadState(NavMeshAgent agent, AnimatorController animatorcontroler, SpawnDrops spawnDrops, Action hitStop):base(hitStop)
        {
            _agent = agent;
            _animatorcontroler = animatorcontroler;
            _spawnDrops = spawnDrops;
        }

        public override void Enter()
        {
            _agent.speed = 0;
            _agent.isStopped = true;
            _animatorcontroler.enabled = false;
            hitStop();
            _spawnDrops.Drop();
        }
    }
}