using Game.Gameplay.Player;

namespace Game.Gameplay.Weapons
{
    public class FlameThrowerWeapon : TriggerWeapon
    {
        PlayerAnimationManager _animationManager;

        public override void SubscribeToAnimationEvents(PlayerAnimationManager animationManager)
        {
            animationManager.AddAnimationEvent("start_reload", EVENT_START_RELOAD);
            animationManager.AddAnimationEvent("fire_shooting_event", EVENT_Weapon_SHOOTING);
            _TriggerAttackAnimation = animationManager.FireShooter;
            _animationManager = animationManager;
        }

        public override void StartSpecial()
        {
        }

        public override void PerformedSpecial()
        {
            _animationManager.AttackFireSpecial();
        }

        public override void EndSpecial()
        {
        }

        void EVENT_START_RELOAD()
        {
            _audioSource.PlayOneShot(_weaponData.ReloadSound);
        }
    }
}