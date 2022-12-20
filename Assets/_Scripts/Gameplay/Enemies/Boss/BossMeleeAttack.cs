using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossMeleeAttack : MonoBehaviour
    {
        [SerializeField] GameObject _slamEffect;
        [SerializeField] Transform _attackPoint;
        [SerializeField] GameObject _boss;

        public void Attack()
        {
            var damage = Instantiate(_slamEffect, _attackPoint.position, Quaternion.identity);
            var damagingStart = damage.GetComponent<DamagingStart>();
            damagingStart.DamagingSource = _boss;
            damagingStart.Start();
            Destroy(damage, 2f);
        }
    }
}
