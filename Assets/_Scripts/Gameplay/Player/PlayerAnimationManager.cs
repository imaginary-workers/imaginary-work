using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] Animator _myAni;
        [SerializeField] WeaponManager _weaponManager;
        readonly Dictionary<string, Action> _events = new Dictionary<string, Action>();

        void Awake()
        {
            _weaponManager.OnWeaponChange += SwitchWeapon;
        }

        public void AddAnimationEvent(string eventName, Action callback)
        {
            if (_events.ContainsKey(eventName))
                _events[eventName] += callback;
            else
                _events.Add(eventName, callback);
        }

        public void RemoveAnimationEvent(string eventName)
        {
            _events.Remove(eventName);
        }

        /**
         * It get call from the animation events
         */
        public void PLAYER_EVENT(string eventName)
        {
            if (!_events.ContainsKey(eventName)) return;
            _events[eventName]?.Invoke();
        }

        public void AttackMelee()
        {
            _myAni.SetTrigger("MeleeTrigger");
        }

        public void StopAttackMelee()
        {
            _myAni.ResetTrigger("MeleeTrigger");
        }

        public void PistolShooter()
        {
            _myAni.SetTrigger("PistolTrigger");
        }

        public void FireShooter()
        {
            _myAni.SetTrigger("FireTrigger");
        }

        public void StartSprint()
        {
            _myAni.SetBool("IsSprinting", true);
        }

        public void StopSprint()
        {
            _myAni.SetBool("IsSprinting", false);
        }

        public void StartReloading()
        {
            _myAni.SetTrigger("ReloadTrigger");
        }

        public void SwitchWeapon()
        {
            _myAni.SetTrigger("WeaponChange");
        }

        public void BackToIdle()
        {
            _myAni.Play("idle");
        }

        public void StrongHitAnimation()
        {
            _myAni.Play("meleeStrongHit");
        }

        public void HitAnimation()
        {
            _myAni.Play("meleeWeakHit");
        }

        public void CancelAttact()
        {
            _myAni.ResetTrigger("MeleeTrigger");
            _myAni.ResetTrigger("PistolTrigger");
            _myAni.ResetTrigger("FireTrigger");
        }

        public void CancelReload()
        {
            _myAni.ResetTrigger("ReloadTrigger");
        }
    }
}