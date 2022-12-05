using Game.Dialogs;
using Game.Gameplay;
using UnityEngine;

namespace Game.Tutorial
{
    public class WeaponUnlonkerComponentDecorator : WeaponUnlonkerComponent
    {
        [SerializeField] private DialogEmitter _dialogEmitter; 
        protected override bool UnlockedWeapon(Collider other)
        {
            var result = base.UnlockedWeapon(other);
            if (result)
            {
                _dialogEmitter.Emit();
            }

            return result;
        }
    }
}