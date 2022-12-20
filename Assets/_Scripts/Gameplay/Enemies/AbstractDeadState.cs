using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public abstract class AbstractDeadState : State
    {
        public GameObject Damaging { get; set; }

        public override void Enter()
        {
            Enemy.SubstractEnemy();
        }
    }
}