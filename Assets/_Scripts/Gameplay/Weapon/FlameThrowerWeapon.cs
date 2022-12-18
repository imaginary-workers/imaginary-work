using Game.Gameplay.Player;
using System;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class FlameThrowerWeapon : TriggerWeapon
    {
        PlayerAnimationManager _animationManager;
        [SerializeField] AudioClip _preSpecial;
        [SerializeField] AudioClip _shootSpecial;

        public override void SubscribeToAnimationEvents(PlayerAnimationManager animationManager)
        {
            animationManager.AddAnimationEvent("start_reload", EVENT_START_RELOAD);
            animationManager.AddAnimationEvent("fire_shooting_event", EVENT_Weapon_SHOOTING);
            animationManager.AddAnimationEvent("fire_special_shooting_event", EVENT_Weapon_SHOOTING_Sound);
            animationManager.AddAnimationEvent("fire_special_start_event", EVENT_Weapon_SHOOTING_START);
            animationManager.AddAnimationEvent("fire_special_end_event", EVENT_Weapon_SHOOTING_END);
            _TriggerAttackAnimation = animationManager.FireShooter;
            _animationManager = animationManager;
        }

        private void EVENT_Weapon_SHOOTING_START()
        {
            _audioSource.PlayOneShot(_preSpecial);
            isSpecial = true;
        }
        private void EVENT_Weapon_SHOOTING_END()
        {
            canAttack = true;
            isSpecial = false;
        }

        public override void StartSpecial()
        {
        }
        public void EVENT_Weapon_SHOOTING_Sound()
        {
            _audioSource.PlayOneShot(_shootSpecial);
            EVENT_Weapon_SHOOTING();
        }
        public override void PerformedSpecial()
        {
            if (!canAttack || _weaponData.Energy != _weaponData.MaxEnergy) return;
            canAttack = false;
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