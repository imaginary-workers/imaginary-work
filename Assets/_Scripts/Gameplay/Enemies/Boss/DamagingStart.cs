using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class DamagingStart : MonoBehaviour
    {
        [SerializeField] LayerMask _layerDamaging;
        [SerializeField] float _radius;
        [SerializeField] int _damage;
        [SerializeField] GameObject _damagingSource;

        public GameObject DamagingSource
        {
            get => _damagingSource != null ? _damagingSource : gameObject;
            set => _damagingSource = value;
        }

        public void Start()
        {
            var colliders = Physics.OverlapSphere(transform.position, _radius, _layerDamaging);
            foreach (var collider in colliders)
            {
                collider.GetComponent<IDamageable>()?.TakeDamage(_damage, null, DamagingSource);
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
