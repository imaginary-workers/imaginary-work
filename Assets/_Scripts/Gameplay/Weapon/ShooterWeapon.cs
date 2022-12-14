using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public abstract class ShooterWeapon : Weapon
    {
        [SerializeField] protected ObjectPooler _bulletPooler;

        [SerializeField] protected Transform _firePoint;

        protected Vector3 ShootDirection
        {
            get
            {
                var distance = Vector3.Distance(Target.position, _firePoint.transform.position);
                if (distance > 4)
                {
                    return (Target.position - _firePoint.transform.position).normalized;
                }
                else
                {
                    return _firePoint.transform.forward;
                }
            }
        }

        protected abstract void Shoot();

        public override bool CanReloadAmmunition()
        {
            if (ReserveAmmunition <= 0 || Ammunition == _weaponData.MaxAmunicion)
                return false;
            return true;
        }

        public override void ReloadAmmunition()
        {
            if (!CanReloadAmmunition()) return;

            var ReserveDif = _weaponData.MaxAmunicion - Ammunition;
            if (ReserveAmmunition >= ReserveDif)
            {
                Ammunition += ReserveDif;
                ReserveAmmunition -= ReserveDif;
            }
            else
            {
                Ammunition = ReserveAmmunition;
                ReserveAmmunition = 0;
            }
        }

        public override bool ReloadReserveAmmunition()
        {
            if (ReserveAmmunition >= _weaponData.MaxReserveAmunicion) return false;

            ReserveAmmunition = _weaponData.MaxReserveAmunicion;

            return true;
        }
    }
}