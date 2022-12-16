using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

namespace Game.Gameplay.Enemies.Boss
{
    public class AttackState : State
    {
         readonly BossStateController _bossStateController;
         readonly AnimatorController _animatorController;
         readonly int _attackCounts;
         private int _attackCounter = 0;
         private float _time;
         private float _waitTime;
         private readonly float _waitBetween;
         private readonly float _waitToIdle;

         public AttackState(BossStateController bossStateController, AnimatorController animatorController, int attackCounts, float waitBetween, float waitToIdle)
         {
             _bossStateController = bossStateController;
             _animatorController = animatorController;
             _attackCounts = attackCounts;
             _waitBetween = waitBetween;
             _waitToIdle = waitToIdle;
         }
        public override void Enter()
        {
            if (Random.Range(0, 2) == 0)
            {
                _animatorController.AttackRigth();
            }
            else
            {
                _animatorController.AttackLeft();
            }
            _time = 0;
            _attackCounter++;
            _waitTime = _attackCounter >= _attackCounts ? _waitToIdle : _waitBetween;
            
        }

        public override void Update()
        {
            _time += Time.deltaTime;
            if (_time >= _waitTime)
            {
                _bossStateController.ChangeState(_bossStateController.IdleState);
                return;
            }
        }

        public override void Exit()
        {
            _animatorController.Idle();
            if (_attackCounter >= _attackCounts)
                _attackCounter = 0;
        }

    }
}
