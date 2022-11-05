using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class MeleeAttackState : State
    {
        FollowMeleeStateController _stateController;
        MeleeAttack _meleeAttack;
        NavMeshAgent _agent;
        LookAtTarget _lookAtTarget;
        AnimationEvent _animationEvent;
        bool _isAttacking = false;
        GameObject _player;
        float _rangeMelee;
        float _rangeOfVisionY;

        public MeleeAttackState(
            FollowMeleeStateController stateController,
            MeleeAttack meleeAttack,
            NavMeshAgent agent,
            LookAtTarget lookAtTarget,
            AnimationEvent animationEvent,
            GameObject player,
            float rangeMelee,
            float rangeOfVisionY
        )
        {
            _stateController = stateController;
            _meleeAttack = meleeAttack;
            _agent = agent;
            _lookAtTarget = lookAtTarget;
            _animationEvent = animationEvent;
            _player = player;
            _rangeMelee = rangeMelee;
            _rangeOfVisionY = rangeOfVisionY;
        }

        public override void Enter()
        {
            _agent.speed = 0;
            _meleeAttack.enabled = true;
            _lookAtTarget.enabled = true;
            _animationEvent.OnAttackStarts += OnOnAttackStartsHandler;
            _animationEvent.OnAttackEnds += OnOnAttackEndsHandler;
            _meleeAttack.Attack();
        }

        public override void Update()
        {
            if (_isAttacking) return;
            Vector3 position = _stateController.transform.position;
            Vector3 playerPosition = _player.transform.position;
            if (!Utils.IsInRangeOfVision(position, playerPosition, _rangeMelee, _rangeOfVisionY))
            {
                _stateController.ChangeState(_stateController.FollowState);
            }
            else
            {
                _meleeAttack.Attack();
            }
        }
        public override void Exit()
        {
            _meleeAttack.enabled = false;
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
