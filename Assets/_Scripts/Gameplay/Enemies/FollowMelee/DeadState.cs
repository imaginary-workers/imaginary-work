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
    }
}
