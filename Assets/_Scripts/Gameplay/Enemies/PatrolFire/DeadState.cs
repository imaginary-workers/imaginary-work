using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class DeadState : State
    {
        NavMeshAgent _agent;
        AnimatorController _animatorController;
        SpawnDrops _spawn;
        float _secondsToDestroy;
        PatrolFireStateController _stateController;
        float _currentSecond = 0f;
        public DeadState(
            PatrolFireStateController stateController,
            AnimatorController animatorController,
            float secondToDestroy,
            NavMeshAgent agent,
            SpawnDrops spawner
            )
        {
            _animatorController = animatorController;
            _secondsToDestroy = secondToDestroy;
            _stateController = stateController;
            _agent = agent;
            _spawn = spawner;
        }

        public override void Enter()
        {
            _agent.speed = 0;
            _agent.isStopped = true;
            _animatorController.Death();
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
            _spawn.Drop();
            _stateController.DestroyGameObject();
        }
    }
}