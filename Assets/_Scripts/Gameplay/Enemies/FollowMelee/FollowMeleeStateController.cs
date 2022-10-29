using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class FollowMeleeStateController : EnemyStateController
    {
        [SerializeField] RandomPatrol _randomPatrol;
        [SerializeField, Range(0, 15)] int _rangeFollow = 15;
        [SerializeField, Range(.1f, 3f)] float _rangeOfVisionY = 1;
        [SerializeField] FollowPlayer _followPlayer;
        [SerializeField] MeleeAttack _meleeAttack;
        [SerializeField] MoveComponent _moveComponent;
        [SerializeField] LookAtTarget _lookAtTarget;
        [SerializeField, Range(0f, 5f)] float _moveSpeed = 5f;
        [SerializeField] EventAnimation _eventAnimation;
        [SerializeField] float _secondToDestroy = 4;
        [SerializeField] SpawnDrops _spawn;
        [SerializeField] MetalEnemyAnimatorController _aniController;
        GameObject _player;
        RandomPatrolState _randomPatrolState;
        FollowState _followState;
        MeleeAttackState _meleeState;
        float _rangeMelee = 0.5f;
        bool _isAttacking;

        public RandomPatrolState RandomPatrolState => _randomPatrolState;
        public FollowState FollowState => _followState;
        public MeleeAttackState MeleeState => _meleeState;
        public int RangeFollow => _rangeFollow;

        protected override void OnAwakeEnemy()
        {
            _player = GameManager.Player;
            _lookAtTarget.Target = _player.gameObject;
            _rangeMelee = _followPlayer.CloseRange;
            _randomPatrol.Speed = _followPlayer.Speed = _moveSpeed;
            _followPlayer.RangeOfVisionY = _rangeOfVisionY;
            _randomPatrolState = new RandomPatrolState(this, _randomPatrol, _player, _rangeOfVisionY);
            _followState = new FollowState(this, _followPlayer, _lookAtTarget, _player, _rangeMelee, _rangeOfVisionY);
            _meleeState = new MeleeAttackState(this, _meleeAttack, _moveComponent, _lookAtTarget, _eventAnimation, _player, _rangeMelee, _rangeOfVisionY);
            _randomPatrol.enabled = false;
            _followPlayer.enabled = false;
            _meleeAttack.enabled = false;
            deadState = new DeadState(_moveComponent, _aniController, _spawn, this, _secondToDestroy);
            ChangeState(_randomPatrolState);
        }
    }

}
