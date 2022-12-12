using Game.SO;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossHealth : MonoBehaviour
    {
        [SerializeField] IntSO bossHealth;
        EnemyDamageable[] _damageablesEnemyParts;

        void Awake()
        {
            _damageablesEnemyParts = GetComponentsInChildren<EnemyDamageable>();
            foreach (var enemyPart in _damageablesEnemyParts)
            {
                enemyPart.Mortal = false;
                enemyPart.OnTakeDamage += OnTakeDamageHandler;
                enemyPart.OnTakeStrongDamage += OnTakeStrongDamageHandler;
            }
        }

        void OnDestroy()
        {
            foreach (var enemyPart in _damageablesEnemyParts)
            {
                enemyPart.OnTakeDamage -= OnTakeDamageHandler;
                enemyPart.OnTakeStrongDamage -= OnTakeStrongDamageHandler;
            }
        }

        void OnTakeStrongDamageHandler(int damage, GameObject arg2)
        {
            bossHealth.value -= damage;
        }

        void OnTakeDamageHandler(int damage, GameObject arg2)
        {
            bossHealth.value -= damage;
            
        }
    }
}
