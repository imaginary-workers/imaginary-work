using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies.FlyerPatrol
{
    public class AttackState : State
    {
        readonly NavMeshAgent _agent;
        readonly RaycastAttack _attack;
        readonly Color _attackColor;
        readonly GameObject _baseCameraObject;
        readonly GameObject _cameraObject;
        readonly Light _focusLight;
        readonly Light _ledLight;
        readonly FlyerPatrolStateController _stateController;
        readonly GameObject _target;
        readonly VisualField _visualField;

        public AttackState(
            FlyerPatrolStateController stateController,
            GameObject target,
            GameObject cameraObject,
            GameObject baseCameraObject,
            RaycastAttack attack,
            NavMeshAgent agent,
            VisualField visualField,
            Light ledLight,
            Light focusLight,
            Color attackColor
        )
        {
            _stateController = stateController;
            _target = target;
            _cameraObject = cameraObject;
            _baseCameraObject = baseCameraObject;
            _attack = attack;
            _agent = agent;
            _visualField = visualField;
            _ledLight = ledLight;
            _focusLight = focusLight;
            _attackColor = attackColor;
        }

        public override void Enter()
        {
            _attack.enabled = true;
            _agent.speed = 0;
            _ledLight.color = _attackColor;
            _focusLight.color = _attackColor;
            _visualField.OnExitViewTarget += ChangeToNormal;
        }

        public override void Update()
        {
            BaseLookTargetXZ();
            CameraLookTargetZY();
        }

        public override void Exit()
        {
            _attack.enabled = false;
            _visualField.OnExitViewTarget -= ChangeToNormal;
        }

        void ChangeToNormal(GameObject obj)
        {
            _stateController.ChangeState(_stateController.NormalState);
        }

        void CameraLookTargetZY()
        {
            var transform = _cameraObject.transform;
            var direction = (_target.transform.position - transform.position).normalized;
            _cameraObject.transform.forward = Vector3.Slerp(transform.forward,
                direction, Time.deltaTime * 3.5f);
        }

        void BaseLookTargetXZ()
        {
            var transform = _baseCameraObject.transform;
            var targetPosition = _target.transform.position;
            var position = transform.position;
            targetPosition.y = position.y;
            var direction = (targetPosition - position).normalized;
            _baseCameraObject.transform.forward = Vector3.Slerp(transform.forward,
                direction, Time.deltaTime * 3.5f);
        }
    }
}