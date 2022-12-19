using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class DamagingStart : MonoBehaviour
    {
        [SerializeField] LayerMask _layerDamaging;
        [SerializeField] float _radius;
        [SerializeField] int _damage;
        [SerializeField] GameObject _damagingSource;

        void OnEnable()
        {
            var colliders = Physics.OverlapSphere(transform.position, _radius, _layerDamaging);
            foreach (var collider in colliders)
            {
                collider.GetComponent<IDamageable>()?.TakeDamage(_damage, null, _damagingSource);
            }
        }

#if UNITY_EDITOR
        [SerializeField] Color _colorGizmo;
        private void OnDrawGizmos()
        {
            Gizmos.color = _colorGizmo;
            Gizmos.DrawSphere(transform.position, _radius);
        }
#endif
    }
}
