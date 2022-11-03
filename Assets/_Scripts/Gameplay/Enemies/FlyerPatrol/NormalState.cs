using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FlyerPatrol
{
    public class NormalState : State
    {        
        FlyerPatrolStateController _stateController;
        NavMeshAgent _agent;
        PatrolBehaviour _patrolBehaviour;
        GameObject _cameraObject;
        GameObject _baseCameraObject;
        VisualField _visualField;
        Light _ledLight;
        Light _focusLight;
        Color _color;

        public NormalState(
            FlyerPatrolStateController stateController,
            NavMeshAgent agent,
            PatrolBehaviour patrolBehaviour,
            GameObject cameraObject,
            GameObject baseCameraObject,
            VisualField visualField,
            Light ledLight,
            Light focusLight,
            Color color
            )
        {
            _stateController = stateController;
            _agent = agent;
            _patrolBehaviour = patrolBehaviour;
            _cameraObject = cameraObject;
            _baseCameraObject = baseCameraObject;
            _visualField = visualField;
            _ledLight = ledLight;
            _focusLight = focusLight;
            _color = color;
        }

        public override void Enter()
        {
            _patrolBehaviour.enabled = true;
            _ledLight.color = _color;
            _focusLight.color = _color;
            _agent.isStopped = false;
            _visualField.OnEnterViewTarget += ChangeToAttack;
        }
        public override void Update()
        {
            BaseLookForward();
            CameraLookForwardDown();
        }

        public override void Exit()
        {
            _patrolBehaviour.enabled = false;
            _agent.isStopped = true;
            _visualField.OnEnterViewTarget -= ChangeToAttack;
        }

        void CameraLookForwardDown()
        {
            _cameraObject.transform.forward = Vector3.Slerp(_cameraObject.transform.forward,
                (_stateController.transform.forward + Vector3.down).normalized, Time.deltaTime * 3.5f);
        }

        void BaseLookForward()
        {
            _baseCameraObject.transform.forward = Vector3.Slerp(_baseCameraObject.transform.forward,
                (_stateController.transform.forward).normalized, Time.deltaTime * 3.5f);
        }

        void ChangeToAttack(GameObject target)
        {
            _stateController.ChangeState(_stateController.AttackState);
        }
    }
}
