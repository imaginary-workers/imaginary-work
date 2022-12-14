using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class EnemyBurstShooter : EnemyShooter
    {
        [SerializeField] float _timeBetweenShoot = .2f;
        float _currentTime;
        bool _shooting;

        void Update()
        {
            if (!_shooting) return;

            if (_currentTime >= _timeBetweenShoot)
            {
                ShootBullet();
                _currentTime = 0;
            }
            else
            {
                _currentTime += Time.deltaTime;
            }
        }

        public void StartBurstShooting()
        {
            _shooting = true;
        }

        public void StopBurstShooting()
        {
            _shooting = false;
        }
    }
}