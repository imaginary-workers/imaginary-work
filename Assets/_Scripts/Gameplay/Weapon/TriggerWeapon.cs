using System;
using System.Collections;
using Game.Gameplay.Player;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class TriggerWeapon : ShooterWeapon
    {
        [SerializeField] ParticleSystem _particles;
        [SerializeField] WeaponsSoundController _weaponSoundController;
        protected Action _TriggerAttackAnimation;
        WaitForSeconds _waitAttackRate;
        void Awake()
        {
            _waitAttackRate = new WaitForSeconds(attackRateInSeconds);
            Ammunition = _weaponData.MaxAmunicion;
            ReserveAmmunition = _weaponData.MaxReserveAmunicion;
        }

        #region public
        public override void StartAttack() { }
        public override void PerformedAttack()
        {
            if (!canAttack || Ammunition <= 0) return;
            canAttack = false;
            _TriggerAttackAnimation.Invoke();
            StartCoroutine(CO_AttackRate());
        }

        IEnumerator CO_AttackRate()
        {
            yield return _waitAttackRate;
            canAttack = true;
        }

        public override void EndAttack() { }
        public override void CancelAttack() { }

        protected void EVENT_Weapon_SHOOTING()
        {
            Shoot();
        }

        #endregion

        protected override void Shoot()
        {
            var bulletObject = _bulletPooler.GetPooledObject();
            bulletObject.transform.position = _firePoint.position;
            bulletObject.SetActive(true);
            bulletObject.transform.forward = _firePoint.forward;
            bulletObject.GetComponent<Bullet>()?.Shoot(ShootDirection);
            Ammunition--;
            GameManager.Instance.UpdateBulletCounter(Ammunition);
            if (_particles != null)
            {
                _particles?.Play();
            }
            IsShoot();
        }


        void IsShoot()
        {
            _audioSource.PlayOneShot(_weaponData.ShootSound);
        }
    }
}