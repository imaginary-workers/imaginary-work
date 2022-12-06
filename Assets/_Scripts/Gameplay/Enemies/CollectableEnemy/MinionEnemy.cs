using Game.Gameplay.Enemies.FollowMelee;
using UnityEngine;

namespace Game.Gameplay.Enemies.CollectableEnemy
{
    public class MinionEnemy : FollowMeleeStateController
    {
        [SerializeField] GameObject _particle;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _audioClip;
        [SerializeField] GameObject _mesh;

        void ActiveDestroyFeedback()
        {
            _audioSource.PlayOneShot(_audioClip);
            Instantiate(_particle, transform.position, Quaternion.identity);
            DestroyGameObject(_audioClip.length);
            Destroy(_mesh);
        }

        protected override void SetDeadState()
        {
            deadState = new DeadState(_agent, _animatorController, _spawn, ActiveDestroyFeedback, _collider);
        }
    }
}