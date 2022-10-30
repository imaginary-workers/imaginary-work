using Game.Gameplay.Enemies;
using UnityEngine;

namespace Game.Gameplay
{
    public class DestructableObject : MonoBehaviour
    {
        [SerializeField] EnemyDamageable _damageable;
        [SerializeField] GameObject _mesh;
        [SerializeField] GameObject _effectPrefab;
        [SerializeField] Transform _effectPoint;
        [SerializeField] SpawnDrops _droper;

        void OnEnable()
        {
            _damageable.OnDeath += DestroyObject;
        }

        void OnDisable()
        {
            _damageable.OnDeath -= DestroyObject;
        }

        void DestroyObject()
        {
            Destroy(_mesh);
            Instantiate(_effectPrefab, _effectPoint.position, Quaternion.identity);
            _droper.Drop();
            Destroy(gameObject);
        }
    }
}
