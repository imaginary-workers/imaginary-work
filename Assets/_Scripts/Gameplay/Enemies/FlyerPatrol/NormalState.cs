using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.FlyerPatrol
{
    public class NormalState : State
    {        
        FlyerPatrolStateController _stateController;
        PatrolBehaviour _patrolBehaviour;
        GameObject _target;
        Vector3 _velocityCheckpoint = Vector3.zero;
        MoveComponent _moveComponent;
        float _maxDistance = 20;
        LookAtTarget _lookAtTarget;

        public NormalState(FlyerPatrolStateController stateController, PatrolBehaviour patrolBehaviour, GameObject target, MoveComponent moveComponent, float maxDistance, LookAtTarget lookAtTarget)
        {
            _stateController = stateController;
            _patrolBehaviour = patrolBehaviour;
            _target = target;
            _moveComponent = moveComponent;
            _maxDistance = maxDistance;
            _lookAtTarget = lookAtTarget;
        }
        public override void Enter()
        {
            _lookAtTarget.transform.forward = Vector3.down;
            _patrolBehaviour.enabled = true;
            if (_velocityCheckpoint != Vector3.zero)
            {
                _moveComponent.Velocity = _velocityCheckpoint;
            }
        }
        public override void Update()
        {
            if (Vector3.Distance(_target.transform.position, _stateController.transform.position) <= _maxDistance)
            {
                _stateController.ChangeState(_stateController.AttackState);
            }
        }
        public override void Exit()
        {
            _patrolBehaviour.enabled = false;
            _velocityCheckpoint = _moveComponent.Velocity;
        }
    }
}
