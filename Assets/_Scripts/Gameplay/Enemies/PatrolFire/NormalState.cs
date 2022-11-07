namespace Game.Gameplay.Enemies.PatrolFire
{
    public class NormalState : State
    {
        VisualField _visualField;
        PatrolBehaviour _patrolBehaviour;
        PatrolFireStateController _stateMachine;

        public NormalState(PatrolFireStateController stateMachine, PatrolBehaviour patrolBehaviour, VisualField visualField)
        {
            _patrolBehaviour = patrolBehaviour;
            _visualField = visualField;
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            _patrolBehaviour.enabled = true;
        }

        public override void Update()
        {
            if (_visualField.IsTargetInView)
                _stateMachine.ChangeState(_stateMachine.AttackState);
        }

        public override void Exit()
        {
            _patrolBehaviour.enabled = false;
        }
    }
}