using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class  Enemy : MonoBehaviour
    {
        public static int countEnemy = 0;
        public static event Action UpdateEnemyCount;

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
        }

        protected virtual void HitStopEffect()
        {
            StartCoroutine(Utils.CO_HitStop(0.3f, 0.001f));
        }
    }
}
