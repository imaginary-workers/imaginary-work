using Game.Gameplay.Enemies;
using UnityEngine;


namespace Game
{
    public class SandSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audio;
        [SerializeField] AudioClip _attack;
        [SerializeField] AudioClip _takeDamage;
        [SerializeField] EnemyDamageable _enemyDamageable;

        private void Awake()
        {
            _enemyDamageable.OnTakeDamage += TakeDamage;
        } 
        public void Attack()
        {
            _audio.PlayOneShot(_attack);
        }
        public void TakeDamage(int damage, GameObject damaging)
        {
            _audio.PlayOneShot(_takeDamage);
        }
    }
}
