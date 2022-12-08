using Game.Gameplay.Player;

namespace Game.Gameplay.Weapons
{
    public class FlameThrowerWeapon : TriggerWeapon
    {
        public override void SubscribeToAnimationEvents(PlayerAnimationManager animationManager)
        {
            animationManager.AddAnimationEvent("start_reload", EVENT_START_RELOAD);
            animationManager.AddAnimationEvent("fire_shooting_event", EVENT_Weapon_SHOOTING);
            _TriggerAttackAnimation = animationManager.FireShooter;
        }

        public override void StartSpecial()
        {
        }

        public override void PerformedSpecial()
        {
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