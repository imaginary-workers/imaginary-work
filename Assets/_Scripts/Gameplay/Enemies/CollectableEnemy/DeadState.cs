using System;
using Game.Gameplay.Enemies.FollowMelee;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.CollectableEnemy
{
    public class DeadState : AbstractDeadState
    {
        readonly NavMeshAgent _agent;
        readonly AnimatorController _animatorcontroler;
        readonly Collider _enemyCollider;
        readonly SpawnDrops _spawnDrops;

        public DeadState(
            NavMeshAgent agent,
            AnimatorController animatorcontroler,
            SpawnDrops spawnDrops,
            Action hitStop,
            Collider enemyCollider
        ) : base(hitStop)
        {
            _agent = agent;
            _animatorcontroler = animatorcontroler;
            _spawnDrops = spawnDrops;
            _enemyCollider = enemyCollider;
        }

        public override void Enter()
        {
            _agent.speed = 0;
            _agent.isStopped = true;
            _animatorcontroler.enabled = false;
            _enemyCollider.enabled = false;
            hitStop();
            _spawnDrops.Drop();
        }
    }
}