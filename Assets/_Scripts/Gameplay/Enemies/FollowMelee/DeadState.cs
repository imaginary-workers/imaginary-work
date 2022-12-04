using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class DeadState : AbstractDeadState
    {
        NavMeshAgent _agent;
        Ragdoll _ragdoll;
        SpawnDrops _spawn;
        float _secondsToDestroy;
        FollowMeleeStateController _stateController;
        float _currentSecond = 0f;
        Collider _enemyCollider;

        public DeadState(
            NavMeshAgent agent,
            Ragdoll ragdoll,
            SpawnDrops spawn,
            FollowMeleeStateController stateController,
            float secondToDestroy,
            Action hitStop,
            Collider enemyCollider

        ): base(hitStop)
        {
            _agent = agent;
            _ragdoll = ragdoll;
            _spawn = spawn;
            _secondsToDestroy = secondToDestroy;
            _stateController = stateController;
            _enemyCollider = enemyCollider;
        }
        public override void Enter()
        {
            _agent.speed = 0;
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
