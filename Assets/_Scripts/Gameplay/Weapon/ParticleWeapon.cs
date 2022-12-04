using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class ParticleWeapon : ShooterWeapon
    {
        [SerializeField] GameObject _particle;
        [SerializeField] bool _isHeavy;
        bool _isShooting;
        float _time;

        void Awake()
        {
            IsHeavy = _isHeavy;
            _time = attackRateInSeconds;
            Ammunition = _weaponData.MaxAmunicion;
            ReserveAmmunition = _weaponData.MaxReserveAmunicion;
            _particle.SetActive(false);
        }

        void Update()
        {
            if (_isShooting)
            {
                if (_time <= 0)
                {
                    Shoot();
                    _time = attackRateInSeconds;
                    Ammunition--;
                    GameManager.Instance.UpdateBulletCounter(Ammunition);
                    if (Ammunition <= 0) EndAttack();
                }
                else
                {
                    _time -= Time.deltaTime;
                }
            }
        }

        protected override void Shoot()
        {
            var bulletObject = _bulletPooler.GetPooledObject();
            bulletObject.SetActive(true);
            bulletObject.transform.position = _firePoint.position;
            bulletObject.GetComponent<Bullet>()?.Shoot(ShootDirection);
        }

        #region public

        public override void StartAttack()
        {
            if (Ammunition <= 0) return;
            _particle.SetActive(true);
            _isShooting = true;
        }

        public override void PerformedAttack()
        {
        }

        public override void EndAttack()
        {
            _particle.SetActive(false);
            _isShooting = false;
        }

        public override void CancelAttack()
        {
            _particle.SetActive(false);
            _isShooting = false;
        }

        #endregion
    }
}