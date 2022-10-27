using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public abstract class  EnemyCount : MonoBehaviour
    {
        public int countEnemy = 0;


        public void AddEnemy()
        {
            countEnemy++;
           
        }
        public void RemoveEnemy()
        {
           
            countEnemy--;
        }
    }
}
