using System;
using Game.Gameplay.Player;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] Damaging _damaging;
        [SerializeField] GameObject player;
        [SerializeField] SwayBehaviour _sway;
        [SerializeField] GameObject _explosion;
        [SerializeField] Transform _pointOfExplosion;
        PlayerAnimationManager _animationManager;
        Vector3 _offset;

        void Awake()
        {
            _damaging?.gameObject.SetActive(false);
            _damaging.OnHit += TriggerOnHitFeedback;
            _damaging.OnStrongHit += TriggerOnStrongHitFeedback;
        }

        void Start()
        {
            _offset = _sway.transform.position;
        }

        void OnDestroy()
        {
            _damaging.OnHit -= TriggerOnHitFeedback;
            _damaging.OnStrongHit -= TriggerOnStrongHitFeedback;
        }

        public override void StartAttack()
        {
        }

        public override void PerformedAttack()
        {
            if (!canAttack) return;
            canAttack = false;
            StartMeleeAnimation();
        }

        public override void EndAttack()
        {
        }

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
            animationManager.AddAnimationEvent("start_special_event", EVENT_START_SPECIAL);
            animationManager.AddAnimationEvent("end_special_event", EVENT_END_SPECIAL);
            animationManager.AddAnimationEvent("spawn_explosion_event", EVENT_SPAWN_EXPLOSION);
        }

        public override void StartSpecial()
        {
        }

        public override void PerformedSpecial()
        {
            if (!canAttack || _weaponData.Energy != _weaponData.MaxEnergy) return;
            canAttack = false;
            StartMeleeSpecialAnimation();
            _weaponData.Energy = 0;
        }

        public override void EndSpecial()
        {
        }

        void EVENT_NO_HIT_FEEDBACK()
        {
            _audioSource.PlayOneShot(_weaponData.NoHitSound);
        }

        void TriggerOnStrongHitFeedback()
        {
            _animationManager.StrongHitAnimation();
        }

        void TriggerOnHitFeedback()
        {
            _animationManager.HitAnimation();
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
        
        void EVENT_START_SPECIAL()
        {
            PlayManager.Instance.SetPlayerControlActive(false, true);
            _sway.transform.position = player.transform.position + _offset;
        }

        void EVENT_SPAWN_EXPLOSION()
        {
            var e = Instantiate(_explosion);
            e.transform.position = _pointOfExplosion.position;
            e.transform.forward = player.transform.forward;
            e.GetComponent<Bullet>()?.Shoot(Vector3.zero);
            //CameraShake
        }

        void EVENT_END_SPECIAL()
        {
            PlayManager.Instance.SetPlayerControlActive(true);
            _sway.enabled = true;
            canAttack = true;
        }

        void StartMeleeAnimation()
        {
            _animationManager.AttackMelee();
        }

        void StartMeleeSpecialAnimation()
        {
            _animationManager.AttackMeleeSpecial();
        }

        void StopMeleeAnimation()
        {
            _animationManager.CancelAttact();
        }

        #endregion
    }
}