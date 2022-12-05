using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Enemy : MonoBehaviour
    {
        static int countEnemy;
        public static int CountEnemy => countEnemy; 

        public static void SubstractEnemy()
        {
            countEnemy--;
            UpdateEnemyCount?.Invoke();
            if (countEnemy == 0) OnNoEnemies?.Invoke();
        }
        internal bool count;

        void Awake()
        {
            countEnemy++;
            UpdateEnemyCount?.Invoke();
            OnAwakeEnemy();
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