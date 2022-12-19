using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossMeleeAttack : MonoBehaviour
    {
        [SerializeField] GameObject _slamEffect;
        [SerializeField] Transform _attackPoint;
        public void Attack()
        {
            var effect = Instantiate(_slamEffect, _attackPoint.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
}
