using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class MeleeAttackState : State
    {
        readonly NavMeshAgent _agent;
        readonly AnimationEvent _animationEvent;
        readonly AnimatorController _animatorController;
        bool _isAttacking;
        readonly LookAtTarget _lookAtTarget;
        readonly GameObject _player;
        readonly float _rangeMelee;
        readonly float _rangeOfVisionY;
        readonly FollowMeleeStateController _stateController;

        public MeleeAttackState(
            FollowMeleeStateController stateController,
            NavMeshAgent agent,
            LookAtTarget lookAtTarget,
            AnimationEvent animationEvent,
            GameObject player,
            float rangeMelee,
            float rangeOfVisionY,
            AnimatorController animatorController
        )
        {
            _stateController = stateController;
            _agent = agent;
            _lookAtTarget = lookAtTarget;
            _animationEvent = animationEvent;
            _player = player;
            _rangeMelee = rangeMelee;
            _rangeOfVisionY = rangeOfVisionY;
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            _agent.speed = 0;
            _lookAtTarget.enabled = true;
            _animationEvent.OnAttackStarts += OnOnAttackStartsHandler;
            _animationEvent.OnAttackEnds += OnOnAttackEndsHandler;
            _animatorController.Attack();
        }

        public override void Update()
        {
            if (_isAttacking) return;

            var position = _stateController.transform.position;
            var playerPosition = _player.transform.position;
            if (!Utils.IsInRangeOfVision(position, playerPosition, _rangeMelee, _rangeOfVisionY))
                _stateController.ChangeState(_stateController.FollowState);
            else
                _animatorController.Attack();
        }

        public override void Exit()
        {
            _lookAtTarget.enabled = false;
            _animationEvent.OnAttackStarts -= OnOnAttackStartsHandler;
            _animationEvent.OnAttackEnds -= OnOnAttackEndsHandler;
        }

        void OnOnAttackEndsHandler()
        {
            _isAttacking = false;
        }

        void OnOnAttackStartsHandler()
        {
            _isAttacking = true;
        }
    }
}