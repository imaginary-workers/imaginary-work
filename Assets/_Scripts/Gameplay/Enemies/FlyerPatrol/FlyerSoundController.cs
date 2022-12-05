using UnityEngine;

namespace Game.Gameplay.Enemies.FlyerPatrol
{
    public class FlyerSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _attack;
        [SerializeField] RaycastAttack _raycastAttack;

        void Awake()
        {
            _raycastAttack.OnAttack += Attack;
        }

        void OnDestroy()
        {
            _raycastAttack.OnAttack -= Attack;
        }

        public void Attack()
        {
            _audioSource.PlayOneShot(_attack);
        }
    }
}