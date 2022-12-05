using System;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public abstract class AbstractDeadState : State
    {
        protected Action hitStop;

        protected AbstractDeadState(Action hitStop)
        {
            this.hitStop = hitStop;
        }

        public GameObject Damaging { get; set; }

        public override void Enter()
        {
            Enemy.SubstractEnemy();
        }
    }
}