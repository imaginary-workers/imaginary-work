using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossMeleeAttack : MonoBehaviour
    {
        [SerializeField] GameObject _slamEffect;
        [SerializeField] GameObject _damaging;
        [SerializeField] Transform _attackPoint;
        public void Attack()
        {
            //Spawnear las particulas
            Instantiate(_slamEffect, _attackPoint.position, Quaternion.identity);
            //Activar zona damaging
            //_damaging.SetActive(true);
        }
    }
}
