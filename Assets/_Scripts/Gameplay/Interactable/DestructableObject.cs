using Game.Gameplay.Enemies;
using UnityEngine;

namespace Game.Gameplay.Interactable
{
    public class DestructableObject : MonoBehaviour
    {
        [SerializeField] EnemyDamageable _damageable;
        [SerializeField] GameObject _mesh;
        [SerializeField] GameObject _effectPrefab;
        [SerializeField] Transform _effectPoint;
        [SerializeField] SpawnDrops _droper;

        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _destroy;

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
            _audioSource.PlayOneShot(_destroy);
            Destroy(_mesh);
            Instantiate(_effectPrefab, _effectPoint.position, Quaternion.identity);
            _droper.Drop();
            Destroy(gameObject,_destroy.length+1);
        }
    }
}
