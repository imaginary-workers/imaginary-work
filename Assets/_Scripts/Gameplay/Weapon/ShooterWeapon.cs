using Game.SO;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public abstract class ShooterWeapon : Weapon
    {
        [SerializeField]
        protected WeaponSO _weaponData;

        [SerializeField]
        protected ObjectPooler _bulletPooler;

        [SerializeField]
        protected Transform _firePoint;

        protected Vector3 ShootDirection
            => (Target - _firePoint.transform.position).normalized;
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