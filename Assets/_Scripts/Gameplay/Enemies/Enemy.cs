using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Enemy : MonoBehaviour
    {
        public static int countEnemy;
        internal bool count;

        void Awake()
        {
            countEnemy++;
            UpdateEnemyCount?.Invoke();
            OnAwakeEnemy();
        }

        void OnDestroy()
        {
            countEnemy--;
            UpdateEnemyCount?.Invoke();
            if (countEnemy == 0) OnNoEnemies?.Invoke();
        }

        public static event Action UpdateEnemyCount;
        public static event Action OnNoEnemies;

        protected abstract void OnAwakeEnemy();

        protected virtual void HitStopEffect()
        {
            StartCoroutine(Utils.CO_HitStop(0.05f, 0.001f));
        }
    }
}