namespace Game.Gameplay.Enemies.PatrolFire
{
    public class NormalState : State
    {
        PatrolFireStateController _stateMachine;
        PatrolBehaviour _patrolBehaviour;
        VisualField _visualField;
        VisualField _visualFieldSound;

        public NormalState(PatrolFireStateController stateMachine, PatrolBehaviour patrolBehaviour, VisualField visualField, VisualField visualFieldSound)
        {
            _stateMachine = stateMachine;
            _patrolBehaviour = patrolBehaviour;
            _visualField = visualField;
            _visualFieldSound = visualFieldSound;
        }

        public override void Enter()
        {
            _patrolBehaviour.enabled = true;
        }

        public override void Update()
        {
            if (_visualField.IsTargetInView || _visualFieldSound.IsTargetInView)
                _stateMachine.ChangeState(_stateMachine.AttackState);
        }

        public override void Exit()
        {
            _patrolBehaviour.enabled = false;
        }
    }
}