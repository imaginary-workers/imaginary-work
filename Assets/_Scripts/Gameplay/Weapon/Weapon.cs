using Game.Gameplay.Player;
using Game.Gameplay.Weapons.SO;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float attackRateInSeconds;

        [SerializeField] protected AudioSource _audioSource;

        [SerializeField] protected WeaponSO _weaponData;

        protected bool canAttack = true;
        public int Ammunition { get; protected set; } = -1;

        [field: SerializeField] public bool IsLocked { get; private set; } = true;

        public int ReserveAmmunition { get; protected set; } = -1;
        public Transform Target { set; protected get; }
        public WeaponSO Data => _weaponData;

        public void UnLocked()
        {
            IsLocked = false;
        }

        public virtual bool CanReloadAmmunition()
        {
            return false;
        }

        public virtual void ReloadAmmunition()
        {
        }

        public virtual bool ReloadReserveAmmunition()
        {
            return false;
        }

        public abstract void StartAttack();
        public abstract void PerformedAttack();
        public abstract void EndAttack();
        public abstract void CancelAttack();

        public virtual void SubscribeToAnimationEvents(PlayerAnimationManager playerAnimationManager)
        {
        }

        public abstract void StartSpecial();
        public abstract void PerformedSpecial();
        public abstract void EndSpecial();
    }
}