using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.PatrolFire
{
    public class TakeStrongDamageState : State
    {
        EnemyStateController _stateController;
        NavMeshAgent _agent;
        AnimatorController _animatorController;

        public TakeStrongDamageState(
            EnemyStateController stateController,
            NavMeshAgent agent,
            AnimatorController animatorController
            )
        {
            _stateController = stateController;
            _agent = agent;
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            // _animationEvent.OnTakeStrongDamageEnds += OnTakeStrongDamageEndsHandler;  
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
            _stateController.ChangeBackToLastState();
        }
    }
}