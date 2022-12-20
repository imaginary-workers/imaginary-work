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
        readonly Action _deadCallback;

        public DeadState(
            NavMeshAgent agent,
            AnimatorController animatorcontroler,
            SpawnDrops spawnDrops,
            Action deadCallback,
            Collider enemyCollider
        )
        {
            _agent = agent;
            _animatorcontroler = animatorcontroler;
            _spawnDrops = spawnDrops;
            _deadCallback = deadCallback;
            _enemyCollider = enemyCollider;
        }

        public override void Enter()
        {
            base.Enter();
            _agent.speed = 0;
            _agent.isStopped = true;
            _animatorcontroler.enabled = false;
            _enemyCollider.enabled = false;
            _deadCallback?.Invoke();
            _spawnDrops.Drop();
        }
    }
}