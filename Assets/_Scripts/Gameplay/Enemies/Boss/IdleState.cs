using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class IdleState : State
    {
        readonly BossStateController _bossStateController;
        float _speed;
        readonly Transform _target;
        readonly Transform _transform;

        public IdleState(BossStateController bossStateController, float speed, Transform target)
        {
            _bossStateController = bossStateController;
            _speed = speed;
            _target = target;
            _transform = _bossStateController.transform;
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
            var targetPosition = _target.position;
            targetPosition.y = _transform.position.y;
            targetPosition.z = _transform.position.z;
            _transform.position = Vector3.Slerp(_transform.position, targetPosition, _speed * Time.deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
