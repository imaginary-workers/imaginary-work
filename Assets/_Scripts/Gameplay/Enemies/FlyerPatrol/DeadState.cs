using Game.Gameplay;
using Game.Gameplay.Enemies;
using Game.Gameplay.Enemies.FlyerPatrol;
using UnityEngine;

namespace Game._Scripts.Gameplay.Enemies.FlyerPatrol
{
    public class DeadState : State
    {
        MoveComponent _moveComponent;
        FlyerPatrolStateController _stateController;

        public DeadState(MoveComponent moveComponent, FlyerPatrolStateController stateController)
        {
            _moveComponent = moveComponent;
            _stateController = stateController;
        }

        public override void Enter()
        {
            _moveComponent.Velocity = Vector3.zero;
        }

        public override void Update()
        {
            Exit();
        }

        public override void Exit()
        {
            _stateController.DestroyGameObject();
        }
    }
}