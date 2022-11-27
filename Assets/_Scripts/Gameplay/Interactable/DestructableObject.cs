using Game.Gameplay.Enemies;
using UnityEngine;

namespace Game.Gameplay.Interactable
{
    public class DestructableObject : MonoBehaviour
    {
        [SerializeField] ObjectDamageable _damageable;
        [SerializeField] GameObject _mesh;
        [SerializeField] GameObject _effectPrefab;
        [SerializeField] Transform _effectPoint;
        [SerializeField] SpawnDrops _droper;

        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _destroy;
        Collider _collider;

        void Start()
        {
            _collider = GetComponent <Collider>();
        }

        void OnEnable()
        {
            _damageable.OnDeath += DestroyThis;
        }

        void OnDisable()
        {
            _damageable.OnDeath -= DestroyThis;
        }

        void DestroyThis(GameObject damage)
        {
            _collider.enabled = false;
            _audioSource.PlayOneShot(_destroy);
            Destroy(_mesh);
            Instantiate(_effectPrefab, _effectPoint.position, Quaternion.identity);
            _droper.Drop();
            Destroy(gameObject,_destroy.length+1);
        }
    }
}
