using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class DeadState : State
    {
        MoveComponent _moveComponent;
        MetalEnemyAnimatorController _animatorController;
        SpawnDrops _spawn;
        float _secondsToDestroy;
        FollowMeleeStateController _stateController;
        float _currentSecond = 0f;

        public DeadState(MoveComponent moveComponent, MetalEnemyAnimatorController animatorController, SpawnDrops spawn, FollowMeleeStateController stateController, float secondToDestroy)
        {
            _moveComponent = moveComponent;
            _animatorController = animatorController;
            _spawn = spawn;
            _secondsToDestroy = secondToDestroy;
            _stateController = stateController;
        }
        public override void Enter()
        {
            _moveComponent.Velocity = Vector3.zero;
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
