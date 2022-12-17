using Game.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class AttackDistanceState : State
    {
        BossStateController _bossStateController;
         AnimatorController _animatorController;
         Transform _firePoint;
         ObjectPooler _bulletPooler;
         string _shootEvent;
         Transform _player;

        public AttackDistanceState(BossStateController bossStateController, AnimatorController animatorController, Transform firePoint, ObjectPooler bulletPooler, string shootEvent) 
        {
            _bossStateController = bossStateController;
            _animatorController = animatorController;
            _firePoint = firePoint;
            _bulletPooler = bulletPooler;
            _shootEvent = shootEvent;
            _player = GameManager.Player.transform;
        }

        public override void Enter()
        {
            _animatorController.AddAnimationEvent(_shootEvent, Shooting);
            _animatorController.Shoot();
        }
 
        public override void Update()
        {

        }

        public override void Exit()
        {
            _animatorController.RemoveAnimationEvent(_shootEvent);
        }
        private void Shooting()
        {
            var position = _player.position;
            position.x = _bossStateController.transform.position.x;
            var direction = position - _firePoint.transform.position;
            var bullet = _bulletPooler.GetPooledObject();
            bullet.transform.position = _firePoint.transform.position;
            bullet.transform.forward = direction.normalized;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>()?.Shoot(direction);
            _bossStateController.ChangeState(_bossStateController.IdleState);
        }
    }
}
