using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class DeadState : AbstractDeadState
    {
        readonly NavMeshAgent _agent;
        float _currentSecond;
        readonly Collider _enemyCollider;
        readonly Ragdoll _ragdoll;
        readonly float _secondsToDestroy;
        readonly SpawnDrops _spawn;
        readonly FollowMeleeStateController _stateController;

        public DeadState(
            NavMeshAgent agent,
            Ragdoll ragdoll,
            SpawnDrops spawn,
            FollowMeleeStateController stateController,
            float secondToDestroy,
            Collider enemyCollider
        )
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
            base.Enter();
            _agent.speed = 0;
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