using System;
using System.Collections;
using Game.Gameplay.Player;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public abstract class TriggerWeapon : ShooterWeapon
    {
        [SerializeField] ParticleSystem _particles;
        [SerializeField] ObjectPooler specialBulletPooler;
        [SerializeField] AudioClip _sound;
        protected Action _TriggerAttackAnimation;
        WaitForSeconds _waitAttackRate;
        protected bool isSpecial = false;

        void Awake()
        {
            _waitAttackRate = new WaitForSeconds(attackRateInSeconds);
            Ammunition = _weaponData.MaxAmunicion;
            ReserveAmmunition = _weaponData.MaxReserveAmunicion;
        }

        protected override void Shoot()
        {
            var pooler = !isSpecial ? _bulletPooler : specialBulletPooler;
            var bulletObject = pooler.GetPooledObject();
            bulletObject.transform.position = _firePoint.position;
            bulletObject.SetActive(true);
            bulletObject.transform.forward = _firePoint.forward;
            bulletObject.GetComponent<Bullet>()?.Shoot(ShootDirection);
            if (!isSpecial)
            {
                Ammunition--;
                GameplayUIManager.Instance.UpdateBulletCounter(Ammunition);
            }
            else _weaponData.Energy = 0;
            if (_particles != null) _particles?.Play();
            IsShoot();
        }


        void IsShoot()
        {
            _audioSource.PlayOneShot(_weaponData.ShootSound);
        }

        #region public

        public override void StartAttack()
        {
        }

        public override void PerformedAttack()
        {
            if (!canAttack || Ammunition <= 0)
            {
                _audioSource.PlayOneShot(_sound);
                return;
            }
            canAttack = false;
            isSpecial = false;
            _TriggerAttackAnimation.Invoke();
            StartCoroutine(CO_AttackRate());
        }

        IEnumerator CO_AttackRate()
        {
            yield return _waitAttackRate;
            canAttack = true;
        }

        public override void EndAttack()
        {
        }

        public override void CancelAttack()
        {
            canAttack = true;
        }

        protected void EVENT_Weapon_SHOOTING()
        {
            Shoot();
        }

        #endregion
    }
}