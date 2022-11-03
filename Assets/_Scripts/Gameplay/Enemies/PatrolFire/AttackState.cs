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
        AnimatorController _animatorController;
        EnemyBurstShooter _enemyShooter;

        public AttackState(
            PatrolFireStateController controller,
            VisualField visualField,
            MoveComponent moveComponent,
            LookAtTarget lookAtTarget,
            AnimatorController animatorController,
            EnemyBurstShooter enemyShooter
            )
        {
            _controller = controller;
            _visualField = visualField;
            _moveComponent = moveComponent;
            _lookAtTarget = lookAtTarget;
            _animatorController = animatorController;
            _enemyShooter = enemyShooter;
            animatorController.AddAnimationEvent("START_SHOOTING", _enemyShooter.StartBurstShooting);
            animatorController.AddAnimationEvent("STOP_SHOOTING", _enemyShooter.StopBurstShooting);
        }

        public override void Enter()
        {
            _moveComponent.Velocity = Vector3.zero;
            _lookAtTarget.enabled = true;
            _animatorController.StartAttack();
            _enemyShooter.enabled = true;
        }
        
        public override void Update()
        {
            if (!_visualField.IsTargetInView)
                _controller.ChangeState(_controller.NormalState);
        }

        public override void Exit()
        {
            _animatorController.StopAttack();
            _lookAtTarget.enabled = false;
            _enemyShooter.enabled = false;
        }
    }
}