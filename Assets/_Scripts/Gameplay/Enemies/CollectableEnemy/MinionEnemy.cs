using Game.Gameplay.Enemies.FollowMelee;
using UnityEngine;

namespace Game.Gameplay.Enemies.CollectableEnemy
{
    public class MinionEnemy: FollowMeleeStateController
    {
        [SerializeField] GameObject _particle;

        protected override void HitStopEffect()
        {
            StartCoroutine(Utils.CO_HitStop(0.3f, 0.001f, ActiveDestroyFeedback));
        }

        void ActiveDestroyFeedback()
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
            DestroyGameObject();
        }

        protected override void SetDeadState()
        {
            deadState = new DeadState(_agent, this, ActiveDestroyFeedback);
        }
    }
}