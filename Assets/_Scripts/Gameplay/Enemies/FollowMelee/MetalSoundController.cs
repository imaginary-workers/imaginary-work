using UnityEngine;

namespace Game.Gameplay.Enemies.FollowMelee
{
    public class MetalSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _WeakDamage;
        [SerializeField] AudioClip _Attack;
        [SerializeField] AudioClip _takeDamageFire;
        [SerializeField] AnimationEvent _aniEvent;
        [SerializeField] EnemyDamageable _enemyDamageable;
        [SerializeField] EnemyDamageable _damageable;

        void Awake()
        {
            _aniEvent.OnAttack += Attack;
            _enemyDamageable.OnTakeDamage += WeakDamage;
            _damageable.OnTakeStrongDamage += StrongTakeDamage;
        }

        void OnDestroy()
        {
            _aniEvent.OnAttack -= Attack;
            _enemyDamageable.OnTakeDamage -= WeakDamage;
        }

        public void WeakDamage(int damage, GameObject damaging)
        {
            _audioSource.PlayOneShot(_WeakDamage);
        }

        public void Attack()
        {
            _audioSource.PlayOneShot(_Attack);
        }

        public void StrongTakeDamage(int damage, GameObject damaging)
        {
            _audioSource.PlayOneShot(_takeDamageFire);
        }
    }
}