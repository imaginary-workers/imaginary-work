using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class RandomPatrolState : State
    {
        readonly PatrolBehaviour _patrolBehaviour;
        readonly GameObject _player;
        readonly float _rangeOfVisionY;
        readonly FollowMeleeStateController _stateController;

        public RandomPatrolState(
            FollowMeleeStateController stateController,
            PatrolBehaviour patrolBehaviour,
            GameObject player,
            float rangeOfVisionY
        )
        {
            _stateController = stateController;
            _patrolBehaviour = patrolBehaviour;
            _player = player;
            _rangeOfVisionY = rangeOfVisionY;
        }

        public override void Enter()
        {
            _patrolBehaviour.enabled = true;
        }

        public override void Update()
        {
            var position = _stateController.transform.position;
            var playerPosition = _player.transform.position;
            if (Utils.IsInRangeOfVision(position, playerPosition, _stateController.RangeFollow, _rangeOfVisionY))
                _stateController.ChangeState(_stateController.FollowState);
        }

        public override void Exit()
        {
            _patrolBehaviour.enabled = false;
        }
    }
}