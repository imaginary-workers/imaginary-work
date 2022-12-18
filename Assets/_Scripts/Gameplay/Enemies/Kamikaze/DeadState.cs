using UnityEngine;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class DeadState : AbstractDeadState
    {
        KamikazeStateController _controller;

        public DeadState(KamikazeStateController controller)
        {
            _controller = controller;
        }

        public override void Enter()
        {
            base.Enter();
          _controller.Explode();
          _controller.DestroyGameObject();
        }
    }
}
