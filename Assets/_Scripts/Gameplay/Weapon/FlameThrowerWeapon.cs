using Game.Gameplay.Player;
using System;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class FlameThrowerWeapon : TriggerWeapon
    {
        PlayerAnimationManager _animationManager;
    

        public override void SubscribeToAnimationEvents(PlayerAnimationManager animationManager)
        {
            animationManager.AddAnimationEvent("start_reload", EVENT_START_RELOAD);
            animationManager.AddAnimationEvent("fire_shooting_event", EVENT_Weapon_SHOOTING);
            animationManager.AddAnimationEvent("fire_special_shooting_event", EVENT_Weapon_SHOOTING);
            animationManager.AddAnimationEvent("fire_special_start_event", EVENT_Weapon_SHOOTING_START);
            animationManager.AddAnimationEvent("fire_special_end_event", EVENT_Weapon_SHOOTING_END);
            _TriggerAttackAnimation = animationManager.FireShooter;
            _animationManager = animationManager;
        }

        private void EVENT_Weapon_SHOOTING_START()
        {
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