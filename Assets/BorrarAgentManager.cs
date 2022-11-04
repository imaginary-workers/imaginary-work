using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Game
{
    public class BorrarAgentManager : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask _layer;
        Ray _ray;
        RaycastHit _hitInfo;
        GameObject[] _ais;

        void Start()
        {
            _ais = GameObject.FindGameObjectsWithTag("AI");
            _hitInfo = new RaycastHit();
            _ray = new Ray();
        }

        public void PointAI(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _ray.origin = _camera.transform.position;
                _ray.direction = _camera.transform.forward;
                if (Physics.Raycast(_ray, out _hitInfo, 100, _layer))
                {
                    foreach (var ai in _ais)
                    {
                        var agent = ai.GetComponent<NavMeshAgent>();
                        agent.SetDestination(_hitInfo.point);
                    }
                }
            }
        }
    }
}
