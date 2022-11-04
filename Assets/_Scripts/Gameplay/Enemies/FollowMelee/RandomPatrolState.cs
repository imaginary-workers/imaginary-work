using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class RandomPatrolState : State
    {
        FollowMeleeStateController _stateController;
        [SerializeField] PatrolBehaviour _patrolBehaviour;
        GameObject _player;
        float _rangeOfVisionY;

        public RandomPatrolState(FollowMeleeStateController stateController, PatrolBehaviour patrolBehaviour, GameObject player, float rangeOfVisionY)
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
            Vector3 position = _stateController.transform.position;
            Vector3 playerPosition = _player.transform.position;
            if (Utils.IsInRangeOfVision(position, playerPosition, _stateController.RangeFollow, _rangeOfVisionY))
            {
                _stateController.ChangeState(_stateController.FollowState);
            }
        }
        public override void Exit()
        {
            _patrolBehaviour.enabled = false;
        }
    }
}
