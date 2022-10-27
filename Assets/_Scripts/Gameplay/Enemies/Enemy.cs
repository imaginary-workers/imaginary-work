using System;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public abstract class  Enemy : MonoBehaviour
    {
        public static int countEnemy = 0;

        void Awake()
        {
            Enemy.countEnemy++;
            OnAwakeEnemy();
        }

        protected abstract void OnAwakeEnemy(); 
        void OnDestroy()
        {
            Enemy.countEnemy--;
        }
       
    }
}
