using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.FlyerPatrol
{
    public class AttackState : State
    {

        FlyerPatrolStateController _stateController;
        LookAtTarget _lookAtTarget;
        RaycastAttack _attack;
        MoveComponent _moveComponent;
        GameObject _target;
        float _maxDistance = 20;

        public AttackState(FlyerPatrolStateController stateController, LookAtTarget lookAtTarget, RaycastAttack attack, MoveComponent moveComponent, GameObject target, float maxDistance)
        {
            _stateController = stateController;
            _lookAtTarget = lookAtTarget;
            _attack = attack;
            _moveComponent = moveComponent;
            _target = target;
            _maxDistance = maxDistance;
            _attack.MaxDistance = _maxDistance;
        }

        public override void Enter()
        {
            _attack.enabled = true;
            _lookAtTarget.enabled = true;
            _moveComponent.Velocity = Vector3.zero;
        }
        public override void Update()
        {
            if (Vector3.Distance(_target.transform.position, _stateController.transform.position) > _maxDistance)
            {
                _stateController.ChangeState(_stateController.NormalState);
            }
        }
        public override void Exit()
        {
            _attack.enabled = false;
            _lookAtTarget.enabled = false;
        }
    }
}
