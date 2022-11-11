using Game.Gameplay.Player;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class ParticleWeapon : ShooterWeapon
    {
        [SerializeField] GameObject _particle;
        [SerializeField] WeaponsSoundController _weaponSoundController;
        bool _isShooting = false;
        float _time;
        [SerializeField] bool _isHeavy;
        
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
            ParticleSound();
            if (_isShooting)
            {
                if (_time <= 0)
                {
                    Shoot();
                    _time = attackRateInSeconds;
                    Ammunition--;
                    GameManager.Instance.UpdateBulletCounter(Ammunition);
                    if(Ammunition <= 0)
                    {
                        CancelAttack();
                    }
                }
                else
                {
                    _time -= Time.deltaTime;
                }
            }
        }
        #region public
        public override void StartAttack()
        {
            if (Ammunition <= 0) return;
            _particle.SetActive(true);
            _isShooting = true;
        }

        public override void PerformedAttack(){ }

        public override void CancelAttack()
        {
            _particle.SetActive(false);
            _isShooting = false;
            _weaponSoundController.ShootFireCanceled();
        }
               
  

        #endregion
        protected override void Shoot()
        {
            
            var bulletObject = _bulletPooler.GetPooledObject();
            bulletObject.SetActive(true);
            bulletObject.transform.position = _firePoint.position;
            bulletObject.GetComponent<Bullet>()?.Shoot(ShootDirection);
        }

        void ParticleSound()
        {
            if(_isShooting)
            _weaponSoundController.ShootFire();
        }
    }
}
