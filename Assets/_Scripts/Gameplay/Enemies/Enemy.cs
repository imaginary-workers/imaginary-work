using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class  Enemy : MonoBehaviour
    {
        public static int countEnemy = 0;
        internal bool count;

        public static event Action UpdateEnemyCount;
        public static event Action OnNoEnemies;

        void Awake()
        {
            Enemy.countEnemy++;
            Enemy.UpdateEnemyCount?.Invoke();
            OnAwakeEnemy();
        }

        protected abstract void OnAwakeEnemy(); 
        void OnDestroy()
        {
            Enemy.countEnemy--;
            Enemy.UpdateEnemyCount?.Invoke();
            if (Enemy.countEnemy == 0)
            {
                Enemy.OnNoEnemies?.Invoke();
            }
        }

        protected virtual void HitStopEffect()
        {
            StartCoroutine(Utils.CO_HitStop(0.05f, 0.001f));
        }
    }
}
