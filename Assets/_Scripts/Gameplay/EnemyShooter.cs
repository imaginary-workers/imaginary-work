using System.Collections;
using UnityEngine;

namespace Game.Gameplay
{
    public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] Transform _firePoint;
        [SerializeField] ObjectPooler _bulletPooler;
        [SerializeField] float _addHighToTarget = 1.5f;
        GameObject _currentBullet;
        GameObject _target;

        public GameObject Target { set => _target = value; }

        public void ShootBullet()
        {
            var pooledObject = _bulletPooler.GetPooledObject();
            var component = pooledObject.GetComponent<Bullet>();
            var transformPosition = _target.transform.position;
            if (_addHighToTarget > 0)
                transformPosition.y += _addHighToTarget;
            var forwardNormalized = (transformPosition - _firePoint.transform.position).normalized;
            component.Shoot(forwardNormalized);
            _currentBullet = null;
        }
    }
}