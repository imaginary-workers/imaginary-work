using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class DeadState : AbstractDeadState
    {
        private BossHealth _bossHealth;

        public DeadState(BossHealth bossHealth)
        {
            _bossHealth = bossHealth;
        }

        public override void Enter()
        {
            base.Enter();
            _bossHealth.EventDead.Raise();
        }

        public override void Update()
        {

        }

        public override void Exit()
        {

        }
    }
}
