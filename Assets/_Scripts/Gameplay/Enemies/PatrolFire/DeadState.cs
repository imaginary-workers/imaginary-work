using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class DeadState : AbstractDeadState
    {
        PatrolFireStateController _stateController;
        Ragdoll _ragdoll;
        float _secondsToDestroy;
        NavMeshAgent _agent;
        SpawnDrops _spawn;
        Collider _enemyCollider;
        float _currentSecond = 0f;

        public DeadState(
            PatrolFireStateController stateController,
            Ragdoll ragdoll,
            float secondToDestroy,
            NavMeshAgent agent,
            SpawnDrops spawner,
            Action hitStop,
            Collider enemyCollider
            ) : base(hitStop)
        {
            _ragdoll = ragdoll;
            _secondsToDestroy = secondToDestroy;
            _stateController = stateController;
            _agent = agent;
            _spawn = spawner;
            _enemyCollider = enemyCollider;
        }

        public override void Enter()
        {
            _agent.speed = 0;
            _agent.isStopped = true;
            _enemyCollider.enabled = false;
            base.Enter();
            _spawn.Drop();
            _ragdoll.SetEnabled(true);
        }

        public override void Update()
        {
            if (_currentSecond < _secondsToDestroy)
            {
                _currentSecond += Time.deltaTime;
            }
            else
            {
                Exit();
            }
        }

        public override void Exit()
        {
            _stateController.DestroyGameObject();
        }
    }
}