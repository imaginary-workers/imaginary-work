using Game.Gameplay.Player;



namespace Game.Gameplay.Weapons
{
    public class NailsWeapon : TriggerWeapon
    {
        public override void SubscribeToAnimationEvents(PlayerAnimationManager animationManager)
        {
            animationManager.AddAnimationEvent("end_reload_weapon", EVENT_RELOAD_FEEDBACK);
            animationManager.AddAnimationEvent("pistol_shooting_event", EVENT_Weapon_SHOOTING);
            _TriggerAttackAnimation = animationManager.PistolShooter;
        }
        void EVENT_RELOAD_FEEDBACK()
        {
            _audioSource.PlayOneShot(_weaponData.ReloadSound);
        }

    }
}
