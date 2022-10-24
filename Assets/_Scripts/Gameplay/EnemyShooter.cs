using System;
using System.Collections;
using UnityEngine;

namespace Game.Gameplay
{
    public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] Transform _firePoint;
        [SerializeField] ObjectPooler _bulletPooler;
        [SerializeField] float _addHighToTarget = 1.5f;
        GameObject _target;

        public GameObject Target { set => _target = value; }

        public void ShootBullet()
        {
            var pooledObject = _bulletPooler.GetPooledObject();
            pooledObject.SetActive(true);
            pooledObject.transform.position = _firePoint.transform.position;
            var component = pooledObject.GetComponent<Bullet>();
            var transformPosition = _target.transform.position;
            if (_addHighToTarget > 0)
                transformPosition.y += _addHighToTarget;
            var forwardNormalized = (transformPosition - pooledObject.transform.position).normalized;
            component.Shoot(forwardNormalized);
        }
    }
}