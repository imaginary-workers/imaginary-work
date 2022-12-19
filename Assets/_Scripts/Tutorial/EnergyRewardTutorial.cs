using Game.Dialogs.SO;
using Game.Gameplay.Enemies;
using UnityEngine;

namespace Game.Tutorial
{
    public class EnergyRewardTutorial : EnergyReward
    {
        [SerializeField] DialogEventSO _tutorialSpecialEvent;
        [SerializeField] DialogSO _tutorialSpecialDialog;

        protected override void AddEnergy(int arg1, GameObject arg2)
        {
            base.AddEnergy(arg1, arg2);
            if (_tutorialSpecialEvent != null)
            {
                _tutorialSpecialEvent.Raise(_tutorialSpecialDialog);
            }
        }
    }
}