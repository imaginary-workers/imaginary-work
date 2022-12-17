using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class AttackDistanceState : State
    {
        public override void Enter()
        {
            Debug.Log("distance");
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            
        }
        public AttackDistanceState(BossStateController bossStateController)
        {
            
        }
    }
}
