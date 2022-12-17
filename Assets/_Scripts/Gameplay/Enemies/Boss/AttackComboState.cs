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

        public AttackComboState(BossStateController bossStateController, AnimatorController animatorController, float waitToIdle)
        {
            _bossStateController = bossStateController;
            _animatorController = animatorController;
            _waitToIdle = waitToIdle;
        }
        public override void Enter()
        {
            Debug.Log("ataque de combo");
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
