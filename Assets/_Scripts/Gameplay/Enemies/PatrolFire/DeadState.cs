using UnityEngine;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class DeadState : State
    {
        PatrolBehaviour _normalBehaviour;
        VisualField _visualField;
        MonoBehaviour _attackBehaviour;  
        MoveComponent _moveComponent;
        ActionRepeater _shooterRepeater;
        LookAtTarget _lookAtTarget;
        EnemyShooter _enemyShooter;
        EnemyDamageable _damageable;
        AnimatorController _animatorController;
        SpawnDrops _spawn;
        GameObject _gameObject;
        float _secondsToDestroy;
        PatrolFireStateController _stateController;

        public DeadState(PatrolFireStateController stateController, AnimatorController animatorController, float secondToDestroy)
        {
            _animatorController = animatorController;
            _secondsToDestroy = 5f;
            _stateController = stateController;
        }

        public override void Enter()
        {
            _moveComponent.Velocity = Vector3.zero;
            _animatorController.Death();
        }

        public override void Update()
        {
            var seconds = 0f;
            if (seconds < _secondsToDestroy)
            {
                seconds += Time.deltaTime;
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