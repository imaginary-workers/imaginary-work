using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] PlayerDamageable _playerdamageable;
        [SerializeField] JumpComponent _jumpComponent;
        [SerializeField] WeaponInventory _weaponInventory;
        [SerializeField] WeaponManager _weaponManager;
        [SerializeField] AudioClip _hurt;
        [SerializeField] AudioClip _walking;
        [SerializeField] AudioClip _jump;
        [SerializeField] AudioClip _eating;
        [SerializeField] AudioClip _dropAmmuation;
        [SerializeField] AudioClip _grab;
        [SerializeField] AudioClip _switch;

        void Awake()
        {
            _jumpComponent.OnJumpEvent += Jump;
            _playerdamageable.OnTakeDamage += Damage;
            _weaponInventory.OnGrab += GrabWeapon;
            _weaponManager.OnWeaponChange += Switch;
        }

        void OnDestroy()
        {
            _playerdamageable.OnTakeDamage -= Damage;
            _jumpComponent.OnJumpEvent -= Jump;
        }

        public void Damage(int damage, GameObject damaging)
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
        public void GrabWeapon()
        {
            _audioSource.PlayOneShot(_grab);
        }
        public void Switch()
        {
            _audioSource.PlayOneShot(_switch);
        }
    }
}
