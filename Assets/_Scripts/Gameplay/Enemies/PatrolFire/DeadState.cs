using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class DeadState : AbstractDeadState
    {
        readonly NavMeshAgent _agent;
        float _currentSecond;
        readonly Collider _enemyCollider;
        readonly Ragdoll _ragdoll;
        readonly float _secondsToDestroy;
        readonly SpawnDrops _spawn;
        readonly PatrolFireStateController _stateController;

        public DeadState(
            PatrolFireStateController stateController,
            Ragdoll ragdoll,
            float secondToDestroy,
            NavMeshAgent agent,
            SpawnDrops spawner,
            Collider enemyCollider
        )
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
            base.Enter();
            _agent.speed = 0;
            _agent.isStopped = true;
            _enemyCollider.enabled = false;
            _spawn.Drop();
            _ragdoll.SetEnabled(true);
            _ragdoll.Knockback(Damaging.transform.forward);
        }

        public override void Update()
        {
            if (_currentSecond < _secondsToDestroy)
                _currentSecond += Time.deltaTime;
            else
                Exit();
        }

        public override void Exit()
        {
            _stateController.DestroyGameObject();
        }
    }
}