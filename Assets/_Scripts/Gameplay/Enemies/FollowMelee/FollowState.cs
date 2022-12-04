using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class FollowState : State
    {
        FollowMeleeStateController _stateController;
        FollowPlayer _followPlayer;
        LookAtTarget _lookAtTarget;
        GameObject _player;
        float _rangeMelee;
        float _rangeOfVisionY;

        public FollowState(
            FollowMeleeStateController stateController,
            FollowPlayer followPlayer,
            LookAtTarget lookAtTarget,
            GameObject player,
            float rangeMelee,
            float rangeOfVisionY
            )
        {
            _stateController = stateController;
            _followPlayer = followPlayer;
            _lookAtTarget = lookAtTarget;
            _player = player;
            _rangeMelee = rangeMelee;
            _rangeOfVisionY = rangeOfVisionY;
        }

        public override void Enter()
        {
            _followPlayer.enabled = true;
            _lookAtTarget.enabled = true;
            
        }
        public override void Update()
        {
            Vector3 position = _stateController.transform.position;
            Vector3 playerPosition = _player.transform.position;
            if (!Utils.IsInRangeOfVision(position, playerPosition, _stateController.RangeFollow, _rangeOfVisionY))
            {
                _stateController.ChangeState(_stateController.RandomPatrolState);
            }
            else if(Utils.IsInRangeOfVision(position, playerPosition, _rangeMelee, _rangeOfVisionY))
            {
                _stateController.ChangeState(_stateController.MeleeState);
            }
        }
        public override void Exit()
        {
            _followPlayer.enabled = false;
            _lookAtTarget.enabled = false;
        }
    }
}
