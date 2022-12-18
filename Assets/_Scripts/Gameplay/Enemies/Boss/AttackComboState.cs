using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class AttackComboState : State
    {
        readonly BossStateController _bossStateController;
        readonly AnimatorController _animatorController;
        readonly float _waitToIdle;
        private string _comboevent;

        public AttackComboState(BossStateController bossStateController, AnimatorController animatorController, float waitToIdle, string comboevent)
        {
            _bossStateController = bossStateController;
            _animatorController = animatorController;
            _waitToIdle = waitToIdle;
            _comboevent = comboevent;
        }



        public override void Enter()
        {
            _animatorController.AddAnimationEvent(_comboevent, ChangeState);
            _animatorController.Combo();
        }


        public override void Update()
        {
           
        }

        public override void Exit()
        {
            _animatorController.RemoveAnimationEvent(_comboevent);
        }
        private void ChangeState()
        {
            _bossStateController.ChangeState(_bossStateController.IdleState);
        }
    }
}
