using System;
using Game.Gameplay.Player;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] Damaging _damaging;
        Action _TriggerAttackAnimation;
        PlayerAnimationManager _animationManager;

        void Awake()
        {
            _damaging?.gameObject.SetActive(false);
            _damaging.OnHit += TriggerOnHitFeedback;
            _damaging.OnStrongHit += TriggerOnStrongHitFeedback;
        }

        private void OnDestroy()
        {
            _damaging.OnHit -= TriggerOnHitFeedback;
            _damaging.OnStrongHit -= TriggerOnStrongHitFeedback;
        }

        public override void StartAttack() { }
        public override void PerformedAttack()
        {
            if (!canAttack) return;
            canAttack = false;
            StartMeleeAnimation();
        }

        public override void EndAttack() { }

        public override void CancelAttack()
        {
            canAttack = true;
            StopMeleeAnimation();
        }

        public override void SubscribeToAnimationEvents(PlayerAnimationManager animationManager)
        {
            _animationManager = animationManager;
            animationManager.AddAnimationEvent("start_melee_heatbox", EVENT_START_HITBOX);
            animationManager.AddAnimationEvent("end_melee_heatbox", EVENT_END_HITBOX);
            animationManager.AddAnimationEvent("end_melee_ani", EVENT_FINISH_ANI);
            animationManager.AddAnimationEvent("no_hit_melee", EVENT_NO_HIT_FEEDBACK);
        }

        void EVENT_NO_HIT_FEEDBACK()
        {
            _audioSource.PlayOneShot(_weaponData.NoHitSound);
        }

        #region Anim Callbacks

        void EVENT_FINISH_ANI()
        {
            canAttack = true;
        }

        void EVENT_END_HITBOX()
        {
            _damaging?.gameObject.SetActive(false);
        }

        void EVENT_START_HITBOX()
        {
            _damaging?.gameObject.SetActive(true);
        }

        void StartMeleeAnimation()
        {
            _animationManager.AttackMelee();
        }

        private void StopMeleeAnimation()
        {
            _animationManager.StopAttackMelee();
        }
        #endregion

        void TriggerOnStrongHitFeedback()
        {
            _animationManager.StrongHitAnimation();//
        }

        void TriggerOnHitFeedback()
        {
            _animationManager.HitAnimation();
        }
    }
}