using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class IdleState : State
    {
        readonly BossStateController _bossStateController;
        private readonly AnimatorController _animatorController;
        float _speed;
        private Vector3 _position;
        readonly Transform _target;
        readonly float _minAttackTime;
        readonly float _maxAttackTime;
        readonly Transform _transform;
        float _time;
        float _timeMax;
        public IdleState(BossStateController bossStateController, AnimatorController animatorController, float speed, Transform target, float minAttackTime, float maxAttackTime)
        {
            _bossStateController = bossStateController;
            _animatorController = animatorController;
            _speed = speed;
            _target = target;
            _minAttackTime = minAttackTime;
            _maxAttackTime = maxAttackTime;
            _transform = _bossStateController.transform;
        }

        public override void Enter()
        {
            _animatorController.Idle();
            _time = 0;
            _position = _transform.position;
            _timeMax = Random.Range(_minAttackTime, _maxAttackTime);
        }

        public override void Update()
        {
            _time += Time.deltaTime;
            if (_time >= _timeMax)
            {
                _bossStateController.ChangeState(_bossStateController.AttackState);
                return;
            }
            var targetPosition = _target.position;
            targetPosition.y = _transform.position.y;
            targetPosition.z = _transform.position.z;
            var position = Vector3.Slerp(_transform.position, targetPosition, _speed * Time.deltaTime);
            position.z = _position.z;
            position.y = _position.y;
            _transform.position = position;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
