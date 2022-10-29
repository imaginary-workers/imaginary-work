using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class TakeStrongDamageState: State
    {
        EnemyStateController _stateController;
        MoveComponent _moveComponent;
        AnimationEvent _animationEvent;
        AnimatorController _animatorController;

        public TakeStrongDamageState(EnemyStateController stateController, MoveComponent moveComponent, AnimationEvent animationEvent, AnimatorController animatorController)
        {
            _stateController = stateController;
            _moveComponent = moveComponent;
            _animationEvent = animationEvent;
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            _animationEvent.OnTakeStrongDamageEnds += OnTakeStrongDamageEndsHandler;  
            _moveComponent.Velocity = Vector3.zero;
            _animatorController.TakeStrongDamageFeedback();
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            _animationEvent.OnTakeStrongDamageEnds -= OnTakeStrongDamageEndsHandler;
        }

        void OnTakeStrongDamageEndsHandler()
        {
            _stateController.ChangeBackToLastState();
        }
    }
}