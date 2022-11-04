using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class DeadState : State
    {
        [SerializeField] NavMeshAgent _agent;
        AnimatorController _animatorController;
        SpawnDrops _spawn;
        float _secondsToDestroy;
        FollowMeleeStateController _stateController;
        float _currentSecond = 0f;

        public DeadState(NavMeshAgent agent, AnimatorController animatorController, SpawnDrops spawn, FollowMeleeStateController stateController, float secondToDestroy)
        {
            _agent = agent;
            _animatorController = animatorController;
            _spawn = spawn;
            _secondsToDestroy = secondToDestroy;
            _stateController = stateController;
        }
        public override void Enter()
        {
            _agent.speed = 0;
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
