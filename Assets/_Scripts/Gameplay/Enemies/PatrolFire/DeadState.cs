using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class DeadState : AbstractDeadState
    {
        NavMeshAgent _agent;
        Ragdoll _ragdoll;
        SpawnDrops _spawn;
        float _secondsToDestroy;
        PatrolFireStateController _stateController;
        float _currentSecond = 0f;

        public DeadState(
            PatrolFireStateController stateController,
            Ragdoll ragdoll,
            float secondToDestroy,
            NavMeshAgent agent,
            SpawnDrops spawner,
            Action hitStop
            ) : base(hitStop)
        {
            _ragdoll = ragdoll;
            _secondsToDestroy = secondToDestroy;
            _stateController = stateController;
            _agent = agent;
            _spawn = spawner;
        }

        public override void Enter()
        {
            _agent.speed = 0;
            _agent.isStopped = true;
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