using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class DamagingStart : MonoBehaviour
    {
        [SerializeField] LayerMask _layerDamaging;
        [SerializeField] float _radius;
        [SerializeField] int _damage;
        [SerializeField] GameObject _damagingSource;
        [SerializeField] Damaging _damaging;
        [SerializeField] Bullet _floorDamaging;

        public GameObject DamagingSource
        {
            get => _damagingSource != null ? _damagingSource : gameObject;
            set
            {
                _damagingSource = value;
                _damaging.EnemySource = value;
            }
        }

        public void Start()
        {
            _floorDamaging.Shoot(Vector3.zero);
            var colliders = Physics.OverlapSphere(transform.position, _radius, _layerDamaging);
            foreach (var collider in colliders)
            {
                collider.GetComponent<IDamageable>()?.TakeDamage(_damage, null, DamagingSource);
            }
        }

#if UNITY_EDITOR
        [SerializeField] Color _colorGizmo;
        [SerializeField] bool _gizmosActive;
        private void OnDrawGizmos()
        {
            if (!_gizmosActive)
            {
                Gizmos.color = _colorGizmo;
                Gizmos.DrawSphere(transform.position, _radius);
            }
        }
#endif
    }
}