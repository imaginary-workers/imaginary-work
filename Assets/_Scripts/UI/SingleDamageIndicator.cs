using System;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public class SingleDamageIndicator : MonoBehaviour
    {
        [SerializeField] DestroyInSecondsComponent _destroy;
        IDamageable _damageable;
        public GameObject Enemy { get; private set; }
        public GameObject Player { get; private set; }

        void Update()
        {
            if (Enemy == null) return;

            PointToDamageOrigin();
        }

        public event Action<SingleDamageIndicator> OnDesactiveIndicator;

        public void Init(GameObject enemy, GameObject player, float maxSeconds)
        {
            Enemy = enemy;
            Player = player;
            _damageable = Enemy.GetComponent<IDamageable>();
            _damageable.OnDeath += DestroyIndicator;
            _destroy.DestroyInSeconds(maxSeconds, () => DestroyIndicator(Enemy));
        }

        public void Reuse(float maxSeconds)
        {
            _destroy.DestroyInSeconds(maxSeconds);
        }

        public void DestroyIndicator(GameObject damaging)
        {
            _damageable.OnDeath -= DestroyIndicator;
            Enemy = null;
            OnDesactiveIndicator?.Invoke(this);
            gameObject.SetActive(false);
        }

        void PointToDamageOrigin()
        {
            var playerTransform = Player.transform;
            var playerPosition = playerTransform.position;
            var damagingPosition = Enemy.transform.position;
            var playerForward = playerTransform.forward;
            var damagingDirection = (damagingPosition - playerPosition).normalized;
            var angle = Vector3.SignedAngle(playerForward, damagingDirection, Vector3.up);
            angle *= -1f;
            var rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = rotation;
        }
    }
}