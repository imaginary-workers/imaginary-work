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
        VisualField _visualField;
        Light _light;
        Color _color;
        Light _lightFocus;

        public AttackState(
            FlyerPatrolStateController stateController,
            LookAtTarget lookAtTarget,
            RaycastAttack attack,
            MoveComponent moveComponent,
            GameObject target,
            VisualField visualField,
            float maxDistance,
            Light light,
            Light lightFocus,
            Color color
            )
        {
            _stateController = stateController;
            _lookAtTarget = lookAtTarget;
            _attack = attack;
            _moveComponent = moveComponent;
            _target = target;
            _visualField = visualField;
            _maxDistance = maxDistance;
            _attack.MaxDistance = _maxDistance;
            _light = light;
            _lightFocus = lightFocus;
            _color = color;
        }

        public override void Enter()
        {
            _attack.enabled = true;
            _lookAtTarget.enabled = true;
            _moveComponent.Velocity = Vector3.zero;
            _light.color = _color;
            _lightFocus.color = _color;
            _visualField.OnExitViewTarget += ChangeToNormal;
        }

        public override void Update()
        {
        }
        public override void Exit()
        {
            _attack.enabled = false;
            _lookAtTarget.enabled = false;
            _visualField.OnExitViewTarget -= ChangeToNormal;
        }

        void ChangeToNormal(GameObject obj)
        {
            _stateController.ChangeState(_stateController.NormalState);
        }
    }
}
