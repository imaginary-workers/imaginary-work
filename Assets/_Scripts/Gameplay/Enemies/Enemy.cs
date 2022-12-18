using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        static int countEnemy;
        public static int CountEnemy => countEnemy;

        public static void ResetEnemyCount()
        {
            countEnemy = 0;
            UpdateEnemyCount?.Invoke();
        }

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
    }
}