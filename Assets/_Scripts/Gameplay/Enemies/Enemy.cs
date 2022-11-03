using System;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
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
       
    }
}
