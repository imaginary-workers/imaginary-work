using UnityEngine;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class DeadState : AbstractDeadState
    {
        KamikazeStateController _controller;
        AudioClip _explosionClip;


        public DeadState(KamikazeStateController controller, AudioClip explosionClip)
        {
            _controller = controller;
            _explosionClip = explosionClip;

        }

        public override void Enter()
        {
            base.Enter();
            AudioSource.PlayClipAtPoint(_explosionClip,_controller.transform.position);
            _controller.Explode();
            _controller.DestroyGameObject();
        }
    }
}
