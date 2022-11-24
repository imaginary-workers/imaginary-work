using Game.Gameplay.Enemies.FollowMelee;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.CollectableEnemy
{
    public class DeadState : AbstractDeadState
    {
        NavMeshAgent _agent;     
        AnimatorController _animatorcontroler;
        SpawnDrops _spawnDrops;
        Collider _enemyCollider;

        public DeadState(
            NavMeshAgent agent,
            AnimatorController animatorcontroler,
            SpawnDrops spawnDrops,
            Action hitStop,
            Collider enemyCollider
        ):base(hitStop)
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