using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class DeadState : State
    {
        KamikazeStateController _controller;

        public DeadState(KamikazeStateController controller)
        {
            _controller = controller;
        }

        public override void Enter()
        {
            Debug.Log("XX");
          _controller.DestroyGameObject();
        }
    }
}
