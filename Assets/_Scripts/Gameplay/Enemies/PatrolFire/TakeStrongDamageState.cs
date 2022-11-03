using UnityEngine;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class TakeStrongDamageState : State
    {
        EnemyStateController _stateController;
        MoveComponent _moveComponent;
        AnimatorController _animatorController;

        public TakeStrongDamageState(
            EnemyStateController stateController,
            MoveComponent moveComponent,
            AnimatorController animatorController
            )
        {
            _stateController = stateController;
            _moveComponent = moveComponent;
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            // _animationEvent.OnTakeStrongDamageEnds += OnTakeStrongDamageEndsHandler;  
            _moveComponent.Velocity = Vector3.zero;
            _animatorController.AddAnimationEvent("TakeStrongDamageEnds", OnTakeStrongDamageEndsHandler);
            _animatorController.TakeStrongDamageFeedback();
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            _animatorController.RemoveAnimationEvent("TakeStrongDamage");
        }

        void OnTakeStrongDamageEndsHandler()
        {
            _stateController.ChangeBackToLastState();
        }
    }
}