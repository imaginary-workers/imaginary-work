using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] PlayerDamageable _playerdamageable;
        [SerializeField] JumpComponent _jumpComponent;
        [SerializeField] AudioClip _hurt;
        [SerializeField] AudioClip _walking;
        [SerializeField] AudioClip _jump;
        [SerializeField] AudioClip _eating;
        [SerializeField] AudioClip _dropAmmuation;

        void Awake()
        {
            _jumpComponent.OnJumpEvent += Jump;
            _playerdamageable.OnTakeDamage += Damage;
        }

        void OnDestroy()
        {
            _playerdamageable.OnTakeDamage -= Damage;
            _jumpComponent.OnJumpEvent -= Jump;
        }

        public void Damage(int damage)
        {
            _audioSource.PlayOneShot(_hurt);
        }
        public void Walking()
        {
            _audioSource.PlayOneShot(_walking);
        }
        public void Jump()
        {
                _audioSource.PlayOneShot(_jump);  
        }
        public void Heal()
        {
            _audioSource.PlayOneShot(_eating);
        }
        public void Amunnition()
        {
            _audioSource.PlayOneShot(_dropAmmuation);
        }
    }
}
