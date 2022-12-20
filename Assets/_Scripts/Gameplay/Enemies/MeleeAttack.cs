using Game.Gameplay.Enemies.FollowMelee;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class MeleeAttack : MonoBehaviour
    {
        [SerializeField] AnimatorController _aniController;

        public void Attack()
        {
            _aniController.Attack();
        }
    }
}