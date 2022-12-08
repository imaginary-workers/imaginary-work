using Game.Gameplay.Player;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class FlameThrowerWeapon : TriggerWeapon
    {
        PlayerAnimationManager _animationManager;
        [SerializeField] ObjectPooler specialBulletPooler;

        public override void SubscribeToAnimationEvents(PlayerAnimationManager animationManager)
        {
            animationManager.AddAnimationEvent("start_reload", EVENT_START_RELOAD);
            animationManager.AddAnimationEvent("fire_shooting_event", EVENT_Weapon_SHOOTING);
            animationManager.AddAnimationEvent("fire_special_shooting_event", EVENT_SPECIAL_WEAPON_SHOOTING);
            _TriggerAttackAnimation = animationManager.FireShooter;
            _animationManager = animationManager;
        }

        private void EVENT_SPECIAL_WEAPON_SHOOTING()
        {
            var bulletObject = specialBulletPooler.GetPooledObject();
            bulletObject.transform.position = _firePoint.position;
            bulletObject.SetActive(true);
            bulletObject.transform.forward = _firePoint.forward;
            bulletObject.GetComponent<Bullet>()?.Shoot(ShootDirection);
            // if (_particles != null) _particles?.Play();
            // IsShoot();
        }

        public override void StartSpecial()
        {
        }

        public override void PerformedSpecial()
        {
            _animationManager.AttackFireSpecial();
        }

        public override void EndSpecial()
        {
        }

        void EVENT_START_RELOAD()
        {
            _audioSource.PlayOneShot(_weaponData.ReloadSound);
        }
    }
}