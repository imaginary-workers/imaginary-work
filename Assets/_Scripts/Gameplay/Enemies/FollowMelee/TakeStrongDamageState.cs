using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class TakeStrongDamageState : State
    {
        readonly NavMeshAgent _agent;
        readonly AnimationEvent _animationEvent;
        readonly AnimatorController _animatorController;
        readonly EnemyStateController _stateController;

        public TakeStrongDamageState(
            EnemyStateController stateController,
            NavMeshAgent agent,
            AnimationEvent animationEvent,
            AnimatorController animatorController
        )
        {
            _stateController = stateController;
            _agent = agent;
            _animationEvent = animationEvent;
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            _animationEvent.OnTakeStrongDamageEnds += OnTakeStrongDamageEndsHandler;
            _agent.speed = 0;
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