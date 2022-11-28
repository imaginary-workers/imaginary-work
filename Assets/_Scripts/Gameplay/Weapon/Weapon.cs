using Game.Gameplay.Player;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] 
        protected float attackRateInSeconds = 0f;
        [SerializeField]
        protected AudioSource _audioSource;
        [SerializeField]
        protected WeaponSO _weaponData;
        protected bool canAttack = true;
        public int Ammunition { get; protected set; } = -1;
        [field: SerializeField]
        public bool IsLocked { get; private set; } = true;
        public void UnLocked() => IsLocked = false;
        public bool IsHeavy { get; protected set; } = false;
        public int ReserveAmmunition { get; protected set; } = -1;
        public Transform Target { set; protected get; }
        public virtual bool CanReloadAmmunition() => false;
        public virtual void ReloadAmmunition() {}
        public virtual bool ReloadReserveAmmunition() => false;
        public abstract void StartAttack();
        public abstract void PerformedAttack();
        public abstract void EndAttack();
        public abstract void CancelAttack();
        public WeaponSO Data { get => _weaponData; }
        public virtual void SubscribeToAnimationEvents(PlayerAnimationManager playerAnimationManager) {}
    }
}