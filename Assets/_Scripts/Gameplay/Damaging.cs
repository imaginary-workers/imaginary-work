using Game.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public class Damaging : MonoBehaviour
    {
        [SerializeField] int _damage = 0;
        [SerializeField] ElementSO _element;

        void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable == null) return;
            
            damageable.TakeTamage(_damage, _element);
            DestroySelf();
        }

        void DestroySelf()
        {
            gameObject.SetActive(false);
        }
    }
}
