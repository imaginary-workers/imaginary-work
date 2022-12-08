using System.Collections;
using Game.Gameplay.Player;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class NailsWeapon : TriggerWeapon
    {
        PlayerAnimationManager _animationManager;
        [SerializeField] float _specialDuration = .5f;
        WaitForSeconds _waitSpecialDuration;

        public override void SubscribeToAnimationEvents(PlayerAnimationManager animationManager)
        {
            _waitSpecialDuration = new WaitForSeconds(_specialDuration);
            animationManager.AddAnimationEvent("end_reload_weapon", EVENT_RELOAD_FEEDBACK);
            animationManager.AddAnimationEvent("pistol_shooting_event", EVENT_Weapon_SHOOTING);
            animationManager.AddAnimationEvent("bullet_reload_weapon", EVENT_RELOAD);
            _animationManager = animationManager;
            _TriggerAttackAnimation = animationManager.PistolShooter;
        }

        void EVENT_RELOAD_FEEDBACK()
        {
            _audioSource.PlayOneShot(_weaponData.ReloadSound);
        }

        void EVENT_RELOAD()
        {
            _audioSource.PlayOneShot(_weaponData.ReloadSoundStart);
        }

        public override void StartSpecial()
        {
        }

        public override void PerformedSpecial()
        {
            if (!canAttack) return;
            canAttack = false;
            isSpecial = true;
            _animationManager.AttackPistolSpecial();
            StartCoroutine(CO_AttackRate());
        }

        public override void EndSpecial()
        {
        }
        
        IEnumerator CO_AttackRate()
        {
            yield return _waitSpecialDuration;
            canAttack = true;
        }
    }
}