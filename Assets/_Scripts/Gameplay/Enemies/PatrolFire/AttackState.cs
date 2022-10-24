using UnityEngine;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class AttackState : State
    {
        VisualField _visualField;
        PatrolFireStateController _controller;
        MoveComponent _moveComponent;
        ActionRepeater _shooterRepeater;
        LookAtTarget _lookAtTarget;
        PatrolFireAnimatorController _animatorController;

        public AttackState(
            PatrolFireStateController controller,
            VisualField visualField,
            MoveComponent moveComponent,
            LookAtTarget lookAtTarget,
            PatrolFireAnimatorController animatorController
            )
        {
            _controller = controller;
            _visualField = visualField;
            _moveComponent = moveComponent;
            _lookAtTarget = lookAtTarget;
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            _moveComponent.Velocity = Vector3.zero;
            _lookAtTarget.enabled = true;
            _animatorController.StartAttack();
        }
        
        public override void Update()
        {
            if (!_visualField.IsTargetInView)
                _controller.ChangeState(_controller.Normal);
        }

        public override void Exit()
        {
            _animatorController.StopAttack();
            _lookAtTarget.enabled = false;
        }
    }
}