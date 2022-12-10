using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.Kamikaze
{
    public class DeadState : State
    {
        KamikazeStateController _controller;

        public override void Enter()
        {
          _controller.DestroyGameObject();
        }
    }
}
