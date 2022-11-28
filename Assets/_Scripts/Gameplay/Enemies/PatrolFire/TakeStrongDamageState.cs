using UnityEngine.AI;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class TakeStrongDamageState : State
    {
        PatrolFireStateController _stateController;
        NavMeshAgent _agent;
        AnimatorController _animatorController;
        VisualField _visualField;
        VisualField _visualFieldSound;

        public TakeStrongDamageState(
            PatrolFireStateController stateController,
            NavMeshAgent agent,
            AnimatorController animatorController,
            VisualField visualField,
            VisualField visualFieldSound
            )
        {
            _stateController = stateController;
            _agent = agent;
            _animatorController = animatorController;
            _visualField = visualField;
            _visualFieldSound = visualFieldSound;
        }

        public override void Enter()
        {
            _agent.speed = 0;
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
            State nextState;
            if (_visualField.IsTargetInView || _visualFieldSound.IsTargetInView)
                nextState = _stateController.AttackState;
            else
                nextState = _stateController.NormalState;
            _stateController.ChangeState(nextState);
        }
    }
}