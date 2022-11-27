using UnityEngine.AI;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class AttackState : State
    {
        PatrolFireStateController _controller;
        VisualField _visualField;
        NavMeshAgent _agent;
        LookAtTarget _lookAtTarget;
        AnimatorController _animatorController;
        EnemyBurstShooter _enemyShooter;
        VisualField _visualdFieldSound;

        public AttackState(
            PatrolFireStateController controller,
            VisualField visualField,
            NavMeshAgent agent,
            LookAtTarget lookAtTarget,
            AnimatorController animatorController,
            EnemyBurstShooter enemyShooter,
            VisualField visualdFieldSound
            )
        {
            _controller = controller;
            _visualField = visualField;
            _agent = agent;
            _lookAtTarget = lookAtTarget;
            _animatorController = animatorController;
            _enemyShooter = enemyShooter;
            _visualdFieldSound = visualdFieldSound;
            animatorController.AddAnimationEvent("START_SHOOTING", _enemyShooter.StartBurstShooting);
            animatorController.AddAnimationEvent("STOP_SHOOTING", _enemyShooter.StopBurstShooting);
        }

        public override void Enter()
        {

            _agent.speed = 0;
            _agent.isStopped = true;
            _lookAtTarget.enabled = true;
            _animatorController.StartAttack();
            _enemyShooter.enabled = true;
        }

        public override void Update()
        {
            if (!_visualField.IsTargetInView && !_visualdFieldSound.IsTargetInView)
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