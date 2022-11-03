using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class RandomPatrolState : State
    {
        FollowMeleeStateController _stateController;
        RandomPatrol _randomPatrol;   
        GameObject _player;
        float _rangeOfVisionY;

        public RandomPatrolState(FollowMeleeStateController stateController, RandomPatrol randomPatrol, GameObject player, float rangeOfVisionY)
        {
            _stateController = stateController;
            _randomPatrol = randomPatrol;
            _player = player;
            _rangeOfVisionY = rangeOfVisionY;
        }

        public override void Enter()
        {
            _randomPatrol.enabled = true;
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
            _randomPatrol.enabled = false;
        }
    }
}
