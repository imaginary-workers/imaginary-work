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
         
        public AttackState(BossStateController bossStateController)
        {
            _bossStateController = bossStateController;
        }
        public override void Enter()
        {
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            base.Exit();
        }

    }
}
