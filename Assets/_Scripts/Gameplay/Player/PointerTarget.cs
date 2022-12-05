using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PointerTarget : MonoBehaviour
    {
        [SerializeField] Camera camera;
        RaycastHit hitInfo;
        Ray ray;

        void Update()
        {
            ray.origin = camera.transform.position;
            ray.direction = camera.transform.forward;
            Physics.Raycast(ray, out hitInfo);
            transform.position = hitInfo.point;
        }
    }
}