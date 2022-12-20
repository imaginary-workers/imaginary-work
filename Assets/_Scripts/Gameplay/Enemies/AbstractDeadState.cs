using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public abstract class AbstractDeadState : State
    {
        int dead = 1;
        public GameObject Damaging { get; set; }

        public override void Enter()
        {
            if (dead < 1) return;
            Enemy.SubstractEnemy();
            dead--;
        }
    }
}