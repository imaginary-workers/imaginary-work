using UnityEngine;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class DeadState : State
    {
        MoveComponent _moveComponent;
        AnimatorController _animatorController;
        SpawnDrops _spawn;
        float _secondsToDestroy;
        PatrolFireStateController _stateController;
        float _currentSecond = 0f;
        public DeadState(PatrolFireStateController stateController, AnimatorController animatorController, float secondToDestroy, MoveComponent moveComponent, SpawnDrops spawner)
        {
            _animatorController = animatorController;
            _secondsToDestroy = 5f;
            _stateController = stateController;
            _moveComponent = moveComponent;
            _spawn = spawner;
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