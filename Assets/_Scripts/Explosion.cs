using EZCameraShake;
using UnityEngine;

namespace Game
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField][Range(0, 10f)] float _magnitud = 1f;
        [SerializeField][Range(0, 10f)] float _roughness = 3f;
        [SerializeField][Range(0, 10f)] float _fadeOutTime = 2f;
        [SerializeField] GameObject _explosion;

        void OnTriggerEnter(Collider other)
        {
            var e = Instantiate(_explosion);
            e.transform.position = transform.position;
            e.transform.localScale = new Vector3(10, 10, 10);
            CameraShaker.Instance.ShakeOnce(_magnitud, _roughness, 0, _fadeOutTime);
        }
    }
}