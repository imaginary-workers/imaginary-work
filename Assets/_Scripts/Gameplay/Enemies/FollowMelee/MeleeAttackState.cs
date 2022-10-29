using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class MeleeAttackState : State
    {
        FollowMeleeStateController _stateController;
        MeleeAttack _meleeAttack;
        MoveComponent _moveComponent;
        LookAtTarget _lookAtTarget;
        EventAnimation _eventAnimation;
        bool _isAttacking = false;
        GameObject _player;
        float _rangeMelee;
        float _rangeOfVisionY;

        public MeleeAttackState(FollowMeleeStateController stateController, MeleeAttack meleeAttack, MoveComponent moveComponent, LookAtTarget lookAtTarget, EventAnimation eventAnimation, GameObject player, float rangeMelee, float rangeOfVisionY)
        {
            _stateController = stateController;
            _meleeAttack = meleeAttack;
            _moveComponent = moveComponent;
            _lookAtTarget = lookAtTarget;
            _eventAnimation = eventAnimation;
            _player = player;
            _rangeMelee = rangeMelee;
            _rangeOfVisionY = rangeOfVisionY;
        }

        public override void Enter()
        {
            _moveComponent.Velocity = Vector3.zero;
            _meleeAttack.enabled = true;
            _lookAtTarget.enabled = true;
            _eventAnimation.OnAttackStarts += () => _isAttacking = true;
            _eventAnimation.OnAttackEnds += () => _isAttacking = false;
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
            _eventAnimation.OnAttackStarts += () => _isAttacking = true;
            _eventAnimation.OnAttackEnds += () => _isAttacking = false;
        }
    }
}
